/*! \file ObjectModelAccess.cs
 *  \brief IObjectModelAccess interface member implementations
 *
 *  This class cannot be instantiated, but can be inherited by other objects with all its methods.
 *  Subclasses need to override the abstract methods.
 *  
 *
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		06/2010
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Features;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpen.Motion;
using System.IO;


namespace Schnittstelle_NX
{
    public class ObjectModelAccess : IObjectModelAccess
    {
        //========================================================================================

        #region Fields

        // The NX session
        private Session m_Session;
        private UFSession m_UFSession;

        // The NX root part
        private Part m_RootPart, m_RootPart_Part;

        // NX status fields
        private PartLoadStatus m_PartLoadStatus;
        private PartSaveStatus m_PartSaveStatus;

        // The part file and simulation name
        private string m_PartFile, m_SimName;

        // Object GUID dictionaries
        protected Dictionary<Component, string> m_ComponentIdDictionary;
        private Dictionary<BodyFeature, string> m_HoleIdDictionary;
        private Dictionary<Joint, string> m_JointIdDictionary;
        private Dictionary<Link, string> m_LinkIdDictionary;
        private Dictionary<Component, string> m_CompIdDictForSync;

        // Flags
        private bool m_IsSimLoaded;

        #endregion

        //========================================================================================

        #region Properties

        public string PartFile
        {
            get { return m_PartFile; }
        }

        public string SimulationName
        {
            get { return m_SimName; }
        }

        #endregion

        //========================================================================================

        #region Construction

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Loads a NX part file.
        /// </summary>
        /// <param name="partPath">The path to a NX part file (.prt)</param>
        //-------------------------------------------------------------------------//
        public ObjectModelAccess(string partPath)
        {
            m_Session = Session.GetSession();
            m_UFSession = UFSession.GetUFSession();
                        
            Open(partPath);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Access to NX simulation information of a given motion file
        /// </summary>
        /// <param name="MotionSimName">The name of the .sim file</param>
        /// <param name="OMA">A ObjectModelAccess_NX object required for general information access</param>
        //-------------------------------------------------------------------------//
        public ObjectModelAccess(string partPath, string motionSimName)
        {
            m_Session = Session.GetSession();
            m_UFSession = UFSession.GetUFSession();

            Open(partPath);
            initComponentsForSync();
            LoadSimulation(motionSimName);
        }

        #endregion

        //========================================================================================

        #region File Processing

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Open a NX part file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Open(string partPath)
        {
            m_PartFile = partPath;
            m_RootPart = m_Session.Parts.Open(partPath, out m_PartLoadStatus);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Load a NX simulation.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void LoadSimulation(string motionSimName)
        {
            m_SimName = motionSimName;

            m_Session.MotionSimulation.LoadSimulation(m_RootPart, m_SimName);
            m_Session.MotionSession.InitializeMechanisms();

            m_RootPart_Part = m_RootPart;
            m_RootPart = m_Session.Parts.Work;

            m_IsSimLoaded = true;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Reinitializes all fields and loads a given part file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Reload(string partFile, bool fSave)
        {
            if (File.Exists(partFile))
            {
                if (fSave) Save();
                CloseAll();
                Open(partFile);

                m_IsSimLoaded = false;
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Reinitializes all fields and loads a given part and simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Reload(string partFile, string simName, bool fSave)
        {
            if (File.Exists(partFile))
            {
                if (fSave) Save();
                CloseAll();
                Open(partFile);

                initComponentsForSync();
                LoadSimulation(simName);

                m_PartFile = partFile;
                m_SimName = simName;
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Save any changes to a NX part and/or simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Save()
        {
            if (m_IsSimLoaded)
            {
                m_Session.MotionSimulation.SaveSimulation(m_RootPart, m_SimName);
                m_PartSaveStatus = m_RootPart_Part.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                m_PartSaveStatus.Dispose();
            }
            else
            {
                m_PartSaveStatus = m_RootPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                m_PartSaveStatus.Dispose();
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Close all parts (even if modified).
        /// </summary>
        //-------------------------------------------------------------------------//
        public void CloseAll()
        {
            if (m_IsSimLoaded)
                m_Session.MotionSimulation.UnloadSimulation(m_RootPart, m_SimName);

            m_Session.Parts.CloseAll(BasePart.CloseModified.CloseModified, null);
        }

        #endregion

        //========================================================================================

        #region IObjectModelAccess Members

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the root component of the current assembly.
        /// </summary>
        /// <returns>The GUID of the root component</returns>
        //-------------------------------------------------------------------------//
        public string GetRootComponent()
        {
            return getRootComponent().GetGuid();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components.
        /// </summary>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetAllComponents()
        {
            List<string> componentIds = new List<string>();
            m_ComponentIdDictionary = new Dictionary<Component, string>();

            foreach (Component c in getAllComponents())
            {
                string id = c.GetGuid();
                m_ComponentIdDictionary.Add(c, id);
                componentIds.Add(id);
            }
            
            return componentIds;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components with given root component.
        /// </summary>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetAllComponents(string rootComponentId)
        {
            List<string> componentIds = new List<string>();
            m_ComponentIdDictionary = new Dictionary<Component, string>();
         
            foreach (Component c in getAllComponents(getComponentById(rootComponentId)))
            {
                string id = c.GetGuid();
                m_ComponentIdDictionary.Add(c, id);
                componentIds.Add(id);
            }

            return componentIds;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all child components of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetAllChildComponents(string componentId)
        {
            return from childComponent in getAllChildComponents(getComponentById(componentId))
                   select childComponent.GetGuid();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the first level child components of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetChildComponents(string componentId)
        {
            return getComponentById(componentId).GetChildren().Select(child => child.GetGuid());
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the parent component of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>One component identifier</returns>
        //-------------------------------------------------------------------------//
        public string GetParentComponent(string componentId)
        {
            Component parent = getComponentById(componentId).Parent;

            if (parent != null)
                return parent.GetGuid();

            return default(string);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the name of a component
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>The name of a component</returns>
        //-------------------------------------------------------------------------//
        public string GetName(string componentId)
        {
            return getComponentById(componentId).DisplayName;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the position of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetPosition(string componentId)
        {
            Point3d point;
            Matrix3x3 matrix;

            getComponentById(componentId).GetPosition(out point, out matrix);

            return new string[] { point.X.ToString(CultureInfo.CurrentCulture), point.Y.ToString(CultureInfo.CurrentCulture), point.Z.ToString(CultureInfo.CurrentCulture) };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the bounding box of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of coordinates (X_min, Y_min, Z_min, X_max, Y_max, Z_max)</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetBoundingBox(string componentId)
        {
            return from value in getBoundingBox(getComponentById(componentId))
                   select value.ToString(CultureInfo.CurrentCulture);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all hole features.
        /// </summary>
        /// <returns>A list of hole feature identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetHoleFeatures()
        {
            m_HoleIdDictionary = new Dictionary<BodyFeature, string>();

            return from component in m_ComponentIdDictionary
                   from holeId in GetHoleFeatures(component.Value)
                   select holeId;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the hole feature of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of hole feature identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetHoleFeatures(string componentId)
        {
            if (m_HoleIdDictionary == null)
                m_HoleIdDictionary = new Dictionary<BodyFeature, string>();

            List<string> holeIds = new List<string>();
            INXObject prototype = getComponentById(componentId).Prototype;

            Part p = prototype as Part;

            if (prototype != null)
            {
                foreach (BodyFeature hole in getHoleFeatures(p))
                {
                    string id = hole.GetGuid();
                    m_HoleIdDictionary.Add(hole, id);
                    holeIds.Add(id);
                }
            }

            return holeIds;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the position of a hole feature.
        /// </summary>
        /// <param name="holeId">The hole feature identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetHolePosition(string holeId)
        {
            Point3d holeLocation = getHoleById(holeId).Location;

            return new string[] { holeLocation.X.ToString(CultureInfo.CurrentCulture), holeLocation.Y.ToString(CultureInfo.CurrentCulture), holeLocation.Z.ToString(CultureInfo.CurrentCulture) };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the direction of a hole feature.
        /// </summary>
        /// <param name="holeId">The hole feature identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetHoleDirection(string holeId)
        {
            Vector3d holeDirection = getHoleDirection(getHoleById(holeId));

            return new string[] { holeDirection.X.ToString(CultureInfo.CurrentCulture), holeDirection.Y.ToString(CultureInfo.CurrentCulture), holeDirection.Z.ToString(CultureInfo.CurrentCulture) };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a list of hole expressions of a given hole, e.g. diameter, depth.
        /// 
        /// Each element contains 3 attributes:
        /// 
        /// [0] = Description
        /// [1] = Value
        /// [2] = Unit
        /// </summary>
        /// <param name="holeId">The hole identifier</param>
        /// <returns>A list of string arrays, which contain hole expressions</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string[]> GetHoleExpressions(string holeId)
        {
            List<string[]> holeExpressions = new List<string[]>();
            Expression[] expressions = getHoleById(holeId).GetExpressions();

            foreach (Expression exp in expressions)
            {
                string description = exp.Description;

                if (string.IsNullOrEmpty(description)) continue;

                string value = exp.Value.ToString();
                string unit = exp.Units.Name;

                holeExpressions.Add(new string[] { description, value, unit });
            }

            return holeExpressions;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the mass of a given component. The mass might be incorrect 
        /// (incomplete), if a component has child components.
        /// 
        /// [0] = the component mass
        /// [1] = boolean; true, if incomplete
        /// [2] = the unit
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>[0] the mass; [1] boolean; true, if mass is incomplete; [2] the unit</returns>
        //-------------------------------------------------------------------------//
        public List<string> GetMassOfComponent(string componentId)
        {
            return getMassOfComponent(getComponentById(componentId)).Select(massInfo => massInfo.ToString()).ToList();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the moments of inertia (centroidal) of a given component 
        /// based on the part coordinate system.
        /// 
        /// key = moments of inertia
        /// value = unit
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of inertia values as string</returns>
        //-------------------------------------------------------------------------//
        public KeyValuePair<string[], string> GetMomentsOfInertia(string componentId)
        {
            var momentsOfInertia = getMomentOfInertia(getComponentById(componentId));

            return new KeyValuePair<string[], string>(momentsOfInertia.Key.Select(inertia => inertia.ToString(CultureInfo.CurrentCulture)).ToArray(), momentsOfInertia.Value);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the length unit of the base part.
        /// </summary>
        /// <returns>The length unit of the base part.</returns>
        //-------------------------------------------------------------------------//
        public string GetPartUnit()
        {
            if (m_RootPart != null)
                return m_RootPart.PartUnits.ToString();
            else return default(string);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a list of all Connector components
        /// </summary>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetAllComponentConnectors()
        {
            return getAllComponentConnectors().Select(comp => comp.GetGuid());
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all components that are connected to an assembly component (screw etc.)
        /// </summary>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetComponentsConnectedWithConnector(string componentId)
        {
            return getCompConnectedWithAssemblyComp(getComponentById(componentId)).Select(comp => comp.GetGuid());
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all links.
        /// </summary>
        /// <returns>A list of link identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetLinks()
        {
            List<string> linkIds = new List<string>();
            m_LinkIdDictionary = new Dictionary<Link, string>();

            foreach (Link link in getAllLinks())
            {
                string id = link.GetGuid();
                m_LinkIdDictionary.Add(link, id);
                linkIds.Add(id);
            }

            return linkIds;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all joints.
        /// </summary>
        /// <returns>A list of joint identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetJoints()
        {
            List<string> jointIds = new List<string>();
            m_JointIdDictionary = new Dictionary<Joint, string>();

            foreach (Joint joint in getAllJoints())
            {
                string id = joint.GetGuid();
                m_JointIdDictionary.Add(joint, id);
                jointIds.Add(id);
            }
            return jointIds;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components of a given link.
        /// </summary>
        /// <param name="linkId">The link identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetComponentsOfLink(string linkId)
        {
            return from component in getComponentsOfLink(getLinkById(linkId))
                   select component.GetGuid();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all links of a given joint.
        /// </summary>
        /// <param name="jointId">The joint identifier</param>
        /// <returns>A list of link identifiers</returns>
        //-------------------------------------------------------------------------//
        public IEnumerable<string> GetLinksOfJoint(string jointId)
        {
            return from link in getLinksOfJoint(getJointById(jointId))
                   where link != null
                   select link.GetGuid();
        }

        #endregion

        //========================================================================================

        #region Extraction methods

        #region get*
        
        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the bounding box of a given component.
        /// </summary>
        /// <param name="c">A NX Component object</param>
        /// <returns>A list of box coordinates</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<double> getBoundingBox(Component c)
        {
            Tag tag;
            List<double[]> box_list = new List<double[]>();

            do
            {
                // Cycle through all object occurrences
                m_UFSession.Assem.CycleObjsInComp(c.Tag, out tag);

                // Get corresponding object of occurrence
                TaggedObject obj = null;

                if (tag != Tag.Null)
                    obj = NXObjectManager.Get(tag);

                // Get the bounding box
                if (obj is Body)
                {
                    double[] bounding_box = new double[6];
                    m_UFSession.Modl.AskBoundingBox(tag, bounding_box);                   
                    box_list.Add(bounding_box);
                }
            }
            while (tag != Tag.Null);

            if (box_list.Count == 0) return new List<double>();

            // Initialize min and max coordinates
            double min_x = double.MaxValue, min_y = double.MaxValue, min_z = double.MaxValue;
            double max_x = double.MinValue, max_y = double.MinValue, max_z = double.MinValue;
            
            // Get absolute min and max coordinates of all bounding boxes
            foreach (double[] box in box_list)
            {
                if (box[0] < min_x) min_x = box[0];
                if (box[1] < min_y) min_y = box[1];
                if (box[2] < min_z) min_z = box[2];

                if (box[3] > max_x) max_x = box[3];
                if (box[4] > max_y) max_y = box[4];
                if (box[5] > max_z) max_z = box[5];
            }
            
            return new double[] { min_x, min_y, min_z, max_x, max_y, max_z };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all hole features of a given nx part.
        /// </summary>
        /// <param name="part">A NX Part object</param>
        /// <returns>A list of NX HoleFeature objects</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<BodyFeature> getHoleFeatures(Part part)
        {
            if (part == null) return default(List<BodyFeature>);


            //Ensure that assembly part is fully loaded. Otherwise features cannot be recognized.
            PartLoadStatus thePartLoadStatus;
            if (!part.IsFullyLoaded) m_Session.Parts.Open(part.FullPath, out thePartLoadStatus);

            ///////////////
            Feature[] features = (from feature in part.Features.ToArray()
                                  select feature).ToArray();
            /*

            Console.WriteLine("-------------------------------------------");

            foreach (Feature feature in features)
            {
                Console.WriteLine(feature.ToString());
                Console.WriteLine("FeatureType: " + feature.FeatureType);
                Console.WriteLine("Name: " + feature.Name);
                Console.WriteLine("FeatureName: " + feature.GetFeatureName());

                string[] msg = feature.GetFeatureInformationalMessages();

                Console.WriteLine("---------------");

                foreach (string message in msg)
                {
                    Console.WriteLine(msg);
                }
            }
             * */
            //////////////////
            return (from feature in part.Features.ToArray()
                    where feature.FeatureType.Contains("HOLE") && feature.ToString().Contains("BodyFeature")
                    select (BodyFeature)feature);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the direction of a given hole feature object.
        /// </summary>
        /// <param name="hole">A NX HoleFeature object</param>
        /// <returns>A list of coordinates</returns>
        //-------------------------------------------------------------------------//
        private Vector3d getHoleDirection(BodyFeature hole)
        {
            Face[] faces = hole.GetFaces();
            Vector3d holeDirection = new Vector3d();

            foreach (Face f in faces)
            {
                int type, norm_dir;
                double[] point = { 0, 0, 0 }, dir = { 0, 0, 0 }, box = { 0, 0, 0, 0, 0, 0 };
                double radius, rad_data;

                m_UFSession.Modl.AskFaceData(f.Tag, out type, point, dir, box, out radius, out rad_data, out norm_dir);

                const int cylinder_face = 16;

                if (type == cylinder_face)
                    holeDirection.X = dir[0]; holeDirection.Y = dir[1]; holeDirection.Z = dir[2];
            }

            return holeDirection;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the mass of a given component.
        /// </summary>
        /// <param name="c">A NX Component object</param>
        /// <returns>A double value, which represents the mass of a given component</returns>
        //-------------------------------------------------------------------------//
        private object[] getMassOfComponent(Component c)
        {
            Part p = null;
            BasePart p_base = null;
            double componentMass = 0;
            bool incompleteMass = false;
            string massUnit = "";

            // Get component part
            if (c.Prototype is Part)
            {
                p = (Part)c.Prototype;
                p_base = (BasePart)p;
            }

            // Compute mass
            if (p != null)
            {
                PartLoadStatus partLoadStatus;

                m_Session.Parts.SetDisplay(p_base, false, false, out partLoadStatus);
                m_Session.Parts.SetWorkComponent(c, out partLoadStatus);

                // Units for the mass
                // [0] = kg
                // [1] = g
                Unit[] allMassUnits = p.UnitCollection.GetMeasureTypes("Mass");

                // Use only kg
                massUnit = allMassUnits[0].Name;
                Unit[] unitArray = { allMassUnits[0] };

                // Compute mass of bodies
                Body[] bodies = p.Bodies.ToArray();

                if (bodies.Length > 0)
                    componentMass = m_Session.Parts.Work.MeasureManager.NewMassProperties(unitArray, 0.999, bodies).Mass;

                // Compute mass of components
                if (c.GetChildren().Length > 0)
                    incompleteMass = true;

                m_Session.Parts.SetDisplay(m_RootPart, false, false, out partLoadStatus);
            }

            return new object[] { componentMass, incompleteMass, massUnit };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the moments of inertia (centroidal) of a given component 
        /// based on the part coordinate system.
        /// 
        /// key = moments of inertia
        /// value = unit
        /// </summary>
        /// <param name="c">A NX component object</param>
        /// <returns>A list of doubles and the unit</returns>
        //-------------------------------------------------------------------------//
        private KeyValuePair<double[], string> getMomentOfInertia(Component c)
        {
            // Mass properties

            // [6-8] = First Moments (centroidal) 
            // [9-11] = Moments Of Inertia, WCS 
            // [12-14] = Moments Of Inertia (centroidal)
            double[] mass_props = new double[47];

            // Array for moments of inertia (kg - m)
            double[] inertia = new double[3];
            inertia[0] = 0.0;
            inertia[1] = 0.0;
            inertia[2] = 0.0;

            // Error messages
            double[] statistics = new double[13];

            // Accuracy
            double[] acc_value = { 0.9 };

            // Required for 'AskMassProps3d' method
            Tag[] bodyTags = new Tag[1];

            string cid = GetName(c.GetGuid());

            // Compute inertia
            if (c.Prototype as Part != null)
            {
                Part p = (Part)c.Prototype;

                foreach (Body b in p.Bodies)
                {
                    bodyTags[0] = b.Tag;

                    /*
                     * Units of mass and length 
                     * 
                     * 1 = Pounds and inches 
                     * 2 = Pounds and feet 
                     * 3 = Grams and centimeters 
                     * 4 = Kilograms and meters
                     * 
                     */
                    if (b.IsSolidBody)
                        m_UFSession.Modl.AskMassProps3d(bodyTags, 1, 1, 4, b.Density, 1, acc_value, mass_props, statistics);
                    else if (b.IsSheetBody)
                        m_UFSession.Modl.AskMassProps3d(bodyTags, 1, 2, 4, b.Density, 1, acc_value, mass_props, statistics);
                    else continue;

                    inertia[0] += mass_props[12];
                    inertia[1] += mass_props[13];
                    inertia[2] += mass_props[14];
                }
            }

            // Return moments of inertia (controidal) based on the part coordinate system
            return new KeyValuePair<double[], string>(inertia, "kg - m2");
        }

        #endregion

        #region get* (components)

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns the root component of the currently loaded loaded assembly.
        /// </summary>
        /// <returns>The root component of the currently loaded assembly.</returns>
        //-------------------------------------------------------------------------//
        private Component getRootComponent()
        {
            return m_RootPart.ComponentAssembly.RootComponent;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all components.
        /// </summary>
        /// <returns>A list of all NX components.</returns>
        //-------------------------------------------------------------------------//
        protected IEnumerable<Component> getAllComponents()
        {
            Component root = getRootComponent();

            List<Component> components = new List<Component>(getAllChildComponents(root));
            components.Insert(0, root);

            return components;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all components with given root component.
        /// </summary>
        /// <returns>A list of all NX components.</returns>
        //-------------------------------------------------------------------------//
        protected IEnumerable<Component> getAllComponents(Component root)
        {
            List<Component> components = new List<Component>(getAllChildComponents(root));
            components.Insert(0, root);

            return components;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all sub components of a given component.
        /// </summary>
        /// <param name="parentComponent">A NX Component object.</param>
        /// <returns>A list of all sub components.</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<Component> getAllChildComponents(Component parentComponent)
        {
            List<Component> components = new List<Component>();

            foreach (Component child in parentComponent.GetChildren())
            {
                components.Add(child);
                components.AddRange(getAllChildComponents(child));
            }

            return components;
        }

        #endregion

        #region get* (kinematics)

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Method that returns all links of the part
        /// </summary>
        /// <returns>links of the part</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<Link> getAllLinks()
        {
            return from link in m_RootPart.MotionManager.Links.ToArray()
                   select link;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Method that returns all joints of the part
        /// </summary>
        /// <returns>joints of the part</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<Joint> getAllJoints()
        {
            return from joint in m_RootPart.MotionManager.Joints.ToArray()
                   select joint;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Method that returns the components of a link
        /// </summary>
        /// <returns>components of the link</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<Component> getComponentsOfLink(Link link)
        {
            UFMotion.Link link_struct;
            m_UFSession.Motion.AskLink(link.Tag, out link_struct);

            return from componentTag in link_struct.geometry
                   select GetComponentByTag(componentTag);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Method that returns links of a joint
        /// </summary>
        /// <returns>joints of the part</returns>
        //-------------------------------------------------------------------------//
        private IEnumerable<Link> getLinksOfJoint(Joint joint)
        {
            UFMotion.Joint joint_struct;
            m_UFSession.Motion.AskJoint(joint.Tag, out joint_struct);

            Link link1 = getLinkByTag(joint_struct.link_1);
            Link link2 = getLinkByTag(joint_struct.link_2);

            return new Link[] { link1, link2 };
        }

        #endregion

        #region get* (connections)

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a list of all Connector components
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Component> getAllComponentConnectors()
        {
            List<Component> componentConnectors_out = new List<Component>();

            foreach (Component comp in m_ComponentIdDictionary.Keys)
                foreach (Component compContained in getAssemblyCompOfComp(comp))
                    if (!componentConnectors_out.Contains(compContained))
                        componentConnectors_out.Add(compContained);

            return componentConnectors_out;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all components that are connected to an assembly component (screw etc.)
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Component> getCompConnectedWithAssemblyComp(Component assemblyComp)
        {
            List<Component> connectedToAssemblyComp = new List<Component>();

            foreach (Component comp in m_ComponentIdDictionary.Keys)
                if (getAssemblyCompOfComp(comp).Contains(assemblyComp))
                    connectedToAssemblyComp.Add(comp);

            return connectedToAssemblyComp;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all assembly components (screws etc.) that are connected to the 
        /// input component
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Component> getAssemblyCompOfComp(Component comp)
        {
            List<Component> assemblyCompList = new List<Component>();
            
            Part p = comp.Prototype as Part;

            if (p != null)
            {
                foreach (BodyFeature hole in getHoleFeatures(p))
                    foreach (Component compContained in getContainmentOfHole(comp, hole))
                        assemblyCompList.Add(compContained);
            }

            return assemblyCompList;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns the components in the hole-centers of a given hole (=AssemblyFeatures). 
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Component> getContainmentOfHole(Component comp, BodyFeature hole)
        {
            List<Component> components = new List<Component>();

            foreach (Point3d point in getCircleCenterOfHole(hole))
            {
                Point3d pointGlobal = mapToGlobalCooridnates(point, comp);

                foreach (Component result in getAllComponentsContainsPoint(pointGlobal))
                    if (!components.Contains(result))
                        components.Add(result);
            }

            return components;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns the component that contains a given point
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Component> getAllComponentsContainsPoint(Point3d point)
        {
            List<Component> assemblyComponents = new List<Component>();
            double[] pointDouble = { point.X, point.Y, point.Z };
            PartLoadStatus thePartLoadStatus;
            int pt_status;

            foreach (Component comp in m_ComponentIdDictionary.Keys)
            {
                //convert to part to access features
                Part part = comp.Prototype as Part;

                if (part != null)
                {
                    //get location of the component
                    Point3d compLocation;
                    Matrix3x3 componentOrient;
                    comp.GetPosition(out compLocation, out componentOrient);

                    //set the part to display part to access all features
                    m_Session.Parts.SetDisplay(part, false, false, out thePartLoadStatus);

                    //we now need the input part in the coordinate system of the input point
                    double[] pointDouble_in_Part_coordinates = { pointDouble[0] - compLocation.X, pointDouble[1] - compLocation.Y, pointDouble[2] - compLocation.Z };

                    //check if any bodies of the the part lie in the considered point
                    foreach (Body body in part.Bodies)
                    {
                        m_UFSession.Modl.AskPointContainment(pointDouble_in_Part_coordinates, body.Tag, out pt_status);
                        
                        // pt_status == 1 means that point is in solid body
                        if (pt_status == 1) assemblyComponents.Add(comp);
                    }
                    //BACK TO ROOT PART
                    m_Session.Parts.SetDisplay(m_RootPart, false, false, out thePartLoadStatus);
                }
            }

            return assemblyComponents;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns the points that are in the center of circular hole edges 
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<Point3d> getCircleCenterOfHole(BodyFeature hole)
        {
            List<Point3d> points = new List<Point3d>();
            IntPtr arc_evaluator;
            UFEval.Arc arc_data; //data each hole edge
            Point3d pointToEvaluate;

            //ensure that asseemblyPart is fully loaded. Otherwise features cannot be recognized.
            PartLoadStatus thePartLoadStatus;
            if (hole.OwningPart.IsFullyLoaded == false)
                m_Session.Parts.Open(hole.OwningPart.FullPath, out thePartLoadStatus);


            foreach (Edge edge in hole.GetEdges())
            {
                m_UFSession.Eval.Initialize(edge.Tag, out arc_evaluator);
                m_UFSession.Eval.AskArc(arc_evaluator, out arc_data);
                pointToEvaluate = new Point3d(arc_data.center[0], arc_data.center[1], arc_data.center[2]);
                points.Add(pointToEvaluate);
            }

            return points;
        }

        #endregion

        #region get* By Id

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get a component by its GUID.
        /// </summary>
        /// <param name="id">The component identifier</param>
        /// <returns>A NX Component object</returns>
        //-------------------------------------------------------------------------//
        protected Component getComponentById(string id)
        {
            return m_ComponentIdDictionary.Where(comp => comp.Value == id).FirstOrDefault().Key;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get a hole feature by its GUID.
        /// </summary>
        /// <param name="holeId">The hole feature identifier</param>
        /// <returns>A NX HoleFeature object</returns>
        //-------------------------------------------------------------------------//
        private BodyFeature getHoleById(string id)
        {
            return m_HoleIdDictionary.Where(hole => hole.Value == id).FirstOrDefault().Key;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a link instance by its GUID.
        /// </summary>
        /// <param name="id">The link identifier</param>
        /// <returns>A link object</returns>
        //-------------------------------------------------------------------------//
        private Link getLinkById(string id)
        {
            return m_LinkIdDictionary.Where(link => link.Value == id).First().Key;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a joint instance by its ID.
        /// </summary>
        /// <param name="id">The joint identifier</param>
        /// <returns>A joint object</returns>
        //-------------------------------------------------------------------------//
        private Joint getJointById(string id)
        {
            return m_JointIdDictionary.Where(joint => joint.Value == id).First().Key;
        }

        #endregion

        #region get* By Tag

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get a component by its tag.
        /// </summary>
        /// <param name="theTag">The component tag</param>
        /// <returns>A NX Component object</returns>
        //-------------------------------------------------------------------------//
        protected static Component GetComponentByTag(Tag theTag)
        {
            if (NXObjectManager.Get(theTag) is Body)
                return ((Body)NXObjectManager.Get(theTag)).OwningComponent;
            else return (Component)NXObjectManager.Get(theTag);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a link instance by its tag.
        /// </summary>
        /// <param name="theTag">The link tag.</param>
        /// <returns>A link instance.</returns>
        //-------------------------------------------------------------------------//
        private Link getLinkByTag(Tag theTag)
        {
            return (Link)NXObjectManager.Get(theTag);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a joint instance by its tag.
        /// </summary>
        /// <param name="theTag">The joint tag.</param>
        /// <returns>A joint instance.</returns>
        //-------------------------------------------------------------------------//
        private Joint getJointByTag(Tag theTag)
        {
            return (Joint)NXObjectManager.Get(theTag);
        }

        #endregion

        #region Vector-Matrix operations

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the global coordinates of a given point with reference component.
        /// </summary>
        /// <param name="p">A NX Point3d object</param>
        /// <param name="c">A NX Component object</param>
        /// <returns>A NX Point3d object</returns>
        //-------------------------------------------------------------------------//
        private Point3d mapToGlobalCooridnates(Point3d p, Component c)
        {
            Point3d c_point;
            Matrix3x3 c_matrix;

            c.GetPosition(out c_point, out c_matrix);

            Vector3d pointZeroVector_local = new Vector3d(p.X, p.Y, p.Z);
            Vector3d componentZeroVector_global = new Vector3d(c_point.X, c_point.Y, c_point.Z);

            Matrix3x3 rotateToGlobalMatrix = transpose(c_matrix);
            Vector3d pointZeroVector_localPosGlobalRot = rotateVector(rotateToGlobalMatrix, pointZeroVector_local);

            Vector3d v1 = componentZeroVector_global;
            Vector3d v2 = pointZeroVector_localPosGlobalRot;

            Vector3d pointZeroVecotr_global = new Vector3d(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

            return new Point3d(pointZeroVecotr_global.X, pointZeroVecotr_global.Y, pointZeroVecotr_global.Z);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the transpose of a given matrix
        /// </summary>
        /// <param name="m">A NX Matrix3x3 object</param>
        /// <returns>A NX Matrix3x3 object</returns>
        //-------------------------------------------------------------------------//
        private Matrix3x3 transpose(Matrix3x3 m)
        {

            Matrix3x3 mT = new Matrix3x3();

            mT.Xx = m.Xx; mT.Xy = m.Yx; mT.Xz = m.Zx;
            mT.Yx = m.Xy; mT.Yy = m.Yy; mT.Yz = m.Zy;
            mT.Zx = m.Xz; mT.Yz = m.Yz; mT.Zz = m.Zz;

            return mT;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get a rotated vector.
        /// </summary>
        /// <param name="m">A NX Matrix3x3 object</param>
        /// <param name="v_in">A NX Vector3d object</param>
        /// <returns>A NX Vector3d object</returns>
        //-------------------------------------------------------------------------//
        private Vector3d rotateVector(Matrix3x3 m, Vector3d v_in)
        {
            double[] v_d = new double[3];

            v_d[0] = m.Xx * v_in.X + m.Xy * v_in.Y + m.Xz * v_in.Z;
            v_d[1] = m.Yx * v_in.X + m.Yy * v_in.Y + m.Yz * v_in.Z;
            v_d[2] = m.Zx * v_in.X + m.Zy * v_in.Y + m.Zz * v_in.Z;

            return new Vector3d(v_d[0], v_d[1], v_d[2]);
        }

        #endregion

        #endregion

        //========================================================================================

        #region Attribute Synchronization

        private void initComponentsForSync()
        {
            // Required to get all component ids, just before a simulation file is loaded
            GetAllComponents();
            m_CompIdDictForSync = new Dictionary<Component, string>(m_ComponentIdDictionary);

            m_PartSaveStatus = m_RootPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
            m_PartSaveStatus.Dispose();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Synchronizes component attributes between simulation and part file.
        /// </summary>
        /// <returns>true, if attribute synchronization has been successful</returns>
        //-------------------------------------------------------------------------//
        private bool synchronizeComponentIds()
        {
            try
            {
                // Load components from part file
                var comps_Model = m_CompIdDictForSync.Keys.ToList();
                var compIds_Model = m_CompIdDictForSync.Values.ToList();
                var compNames_Model = comps_Model.Select(comp => comp.DisplayName).ToList();

                // Load components from simulation file
                var comps_Sim = getAllComponents().ToList();
                var compIds_Sim = GetAllComponents().ToList();
                var compNames_Sim = comps_Sim.Select(comp => comp.DisplayName).ToList();

                for (int i = 0; i < comps_Model.Count; i++)
                {
                    try
                    {
                        // Load component to synchronize with
                        Component comp_Model = comps_Model[i];
                        string compId_Model = compIds_Model[i];
                        string compName_Model = compNames_Model[i];

                        for (int j = 1; j < comps_Sim.Count; j++)
                        {
                            // Load component, which is going to be synchronized
                            Component comp_Sim = comps_Sim[j];
                            string compId_Sim = compIds_Sim[j];
                            string compName_Sim = compNames_Sim[j];

                            // Check, if the names of both components are equal
                            if (compName_Model == compName_Sim)
                            {
                                Point3d compPos_Model, compPos_Sim;
                                Matrix3x3 compOrient_Model, compOrient_Sim;

                                comp_Model.GetPosition(out compPos_Model, out compOrient_Model);
                                comp_Sim.GetPosition(out compPos_Sim, out compOrient_Sim);

                                // Check if both components are really placed on the same position
                                if (
                                    compPos_Model.X == compPos_Sim.X &&
                                    compPos_Model.Y == compPos_Sim.Y &&
                                    compPos_Model.Z == compPos_Sim.Z
                                    )
                                    // Check if they have the same orientation
                                    if (
                                        compOrient_Model.Xx == compOrient_Sim.Xx &&
                                        compOrient_Model.Xy == compOrient_Sim.Xy &&
                                        compOrient_Model.Xz == compOrient_Sim.Xz &&
                                        compOrient_Model.Yx == compOrient_Sim.Yx &&
                                        compOrient_Model.Yy == compOrient_Sim.Yy &&
                                        compOrient_Model.Yz == compOrient_Sim.Yz &&
                                        compOrient_Model.Zx == compOrient_Sim.Zx &&
                                        compOrient_Model.Zy == compOrient_Sim.Zy &&
                                        compOrient_Model.Zz == compOrient_Sim.Zz)
                                    {
                                        // Copy the GUID and set or overwrite the "ID" attribute
                                        comp_Sim.SetGuid(compId_Model);
                                        break;
                                    }
                            }
                            else continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion

        //========================================================================================
    }
}