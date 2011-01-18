/*! \file InformationModelAccess.cs
 *  \brief Allows access to the information model and database.
 *
 *  This class contains all methods, which are invoked, if they are listed in the configuration
 *  file. This is the only class, which has access to the information model (or to the database).
 *
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		08/2010
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using OntologyAccessHelper;


namespace Schnittstelle_NX
{
    public class InformationModelAccess : IInformationModelAccess
    {
        //========================================================================================

        #region Fields

        //Required to store all information and to generate triples
        private SesameAccessHelper s_Controller;

        //The namespaces
        private string
            m_BaseNs, m_BaseNsShort,
            m_GeoNs, m_GeoNsShort,
            m_DynNs, m_DynNsShort,
            m_RdfNs, m_RdfNsShort,
            m_UnitsNs, m_UnitsNsShort,
            m_AssemNs, m_AssemNsShort;

        private string 
            m_PartFile, // the NX part file
            m_DefaultOwlDir, // default owl directory
            m_RootComponentId; // the root component GUID

        //Ontology configuration (see 'Schnittstelle_NX.config.xml')
        private static Config s_Config = new Config();

        //The NX access controllers
        private ObjectModelAccess m_ObjectModelController;

        // Flags
        private bool m_ChildComponentsAdded, m_HoleFeaturesAdded;

        // Pending methods
        private List<string> m_PendingMethods;

        // Current progress
        private double m_Count, m_Index;

        // Component GUID list
        private List<string> m_AllComponents;

        #endregion

        //========================================================================================

        #region Properties

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the progress of the current operation (in percent).
        /// </summary>
        //-------------------------------------------------------------------------//
        public int CurrentProgress
        {
            get
            {
                if (m_Count == 0) return 0;
                return (int)(m_Index / m_Count * 100);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the count of objects in the current operation.
        /// </summary>
        //-------------------------------------------------------------------------//
        public int Count
        {
            get { return (int)m_Count; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the current progress of a running operation.
        /// </summary>
        //-------------------------------------------------------------------------//
        public int CurrentIndex
        {
            get { return (int)m_Index; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Set the object model access controller.
        /// </summary>
        //-------------------------------------------------------------------------//
        public ObjectModelAccess ObjectModelController
        {
            set { m_ObjectModelController = value; }
        }
        
        #endregion

        //========================================================================================

        #region Construction / Initialization

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Connects with the Ontology database and loads the configuration file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public InformationModelAccess(ObjectModelAccess objectModelController, bool fConnect)
        {
            // Store file paths in local variables
            m_PartFile = objectModelController.PartFile;

            // Make ObjectModelAccess objects local
            m_ObjectModelController = objectModelController;

            // Set the base model directory
            m_DefaultOwlDir = s_Config.DefaultOwlDirectory;
            
            // Initialize the Memory Repository
            if (fConnect) Connect();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Initialize fields.
        /// </summary>
        //-------------------------------------------------------------------------//
        private void init()
        {
            // Get the root component id
            m_RootComponentId = m_ObjectModelController.GetRootComponent();
            m_AllComponents = m_ObjectModelController.GetAllComponents().ToList();
            m_PendingMethods = new List<string>();
        }

        #endregion

        //========================================================================================

        #region Repository operations

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Load the global namespaces from the confiuration file
        /// </summary>
        //-------------------------------------------------------------------------//
        private void loadNamespaces()
        {
            s_Controller.GetNamespaces();
            
            // Retrieve namespaces from config file
            m_BaseNs = s_Config.BaseNamespace + "#";
            m_BaseNsShort = s_Controller.GetCorrespondingShortname(m_BaseNs);

            m_GeoNs = s_Config.GeometryNamespace + "#";
            m_GeoNsShort = s_Controller.GetCorrespondingShortname(m_GeoNs);

            m_DynNs = s_Config.DynamicsNamespace + "#";
            m_DynNsShort = s_Controller.GetCorrespondingShortname(m_DynNs);

            m_RdfNs = s_Config.RdfNamespace + "#";
            m_RdfNsShort = s_Controller.GetCorrespondingShortname(m_RdfNs);

            m_UnitsNs = s_Config.UnitsNamespace + "#";
            m_UnitsNsShort = s_Controller.GetCorrespondingShortname(m_UnitsNs);

            m_AssemNs = s_Config.AssemblyNamespace + "#";
            m_AssemNsShort = s_Controller.GetCorrespondingShortname(m_AssemNs);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Load relevant owl files into repository
        /// </summary>
        //-------------------------------------------------------------------------//
        private void loadOwlFiles()
        {
            string[] arrOwlFiles = Directory.GetFiles(m_DefaultOwlDir, "*.owl", SearchOption.TopDirectoryOnly);

            foreach (string owlFile in arrOwlFiles)
                s_Controller.LoadRDFXMLFile(owlFile);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Export all data from ontology into a owl file (RDF/XML)
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Export()
        {
            string exportDir = Path.Combine(m_DefaultOwlDir, s_Config.DefaultExportDirectory);

            if (!Directory.Exists(exportDir))
                Directory.CreateDirectory(exportDir);
            s_Controller.ExportRDFXML(Path.Combine(exportDir, Path.GetFileNameWithoutExtension(m_PartFile) + ".owl"));
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Export all data from ontology into a owl file (RDF/XML)
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Export(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            else if (File.Exists(filePath)) File.Delete(filePath);

            s_Controller.ExportRDFXML(filePath);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Connect to the memory repository.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Connect()
        {
            s_Controller = new SesameAccessHelper();

            loadOwlFiles();
            loadNamespaces();

            init();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Connect to the memory repository.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void Connect(string customOwlDir)
        {
            m_DefaultOwlDir = customOwlDir;
            Connect();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the unit name, which is specified in the information model.
        /// </summary>
        /// <param name="unit">The NX unit name</param>
        /// <returns>The unit name of the information model</returns>
        //-------------------------------------------------------------------------//
        public string GetUnitName(string unit)
        {
            switch (unit)
            {
                case "mm":
                case "Millimeters":
                case "MilliMeter":
                    return s_Config.Unit.MilliMeter;
                case "degrees":
                case "Degrees":
                    return s_Config.Unit.Degree;
                case "Kilogram":
                    return s_Config.Unit.Kilogram;
                case "kg - m2":
                    return s_Config.Unit.KilogramMilliMeterSquare;
                default:
                    return unit;
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Writes or updates the configuration file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public static void WriteConfiguration()
        {
            s_Config.WriteConfiguration();
        }

        #endregion

        //========================================================================================

        #region IInformationModelAccess Members

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all component identifiers of an assembly to the ontology.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddComponents()
        {
            s_Controller.ActivateStandardNamespace(m_BaseNsShort);

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
                s_Controller.CreateIndividual(componentId, s_Config.Class.PhysicalComponent);

            s_Controller.DeactivateStandardNamespace();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds information about child components.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddChildComponents()
        {
            s_Controller.ActivateStandardNamespace(m_BaseNsShort);

            foreach (string parentId in m_ObjectModelController.GetAllComponents())
                foreach (string childId in m_ObjectModelController.GetAllChildComponents(parentId))
                    s_Controller.CreateObjectPropertyForIndividual(parentId, s_Config.ObjectProperty.hasSubEntity, childId);

            m_ChildComponentsAdded = true;

            s_Controller.DeactivateStandardNamespace();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds information about parent components.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddParentComponents()
        {
            s_Controller.ActivateStandardNamespace(m_BaseNsShort);

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
                if (m_ObjectModelController.GetParentComponent(componentId) != default(string))
                    s_Controller.CreateObjectPropertyForIndividual(componentId, s_Config.ObjectProperty.hasSuperEntity, m_ObjectModelController.GetParentComponent(componentId));

            s_Controller.DeactivateStandardNamespace();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Since every component is represented by an ID, this method adds the
        /// display name of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddComponentNames()
        {
            string className = s_Config.Class.NameInAssembly;

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                string nameId = m_GeoNs + className + "-" + Guid.NewGuid();

                s_Controller.CreateIndividual(nameId, m_GeoNs + className);
                
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, nameId);
                s_Controller.CreateObjectPropertyForIndividual(nameId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);

                s_Controller.CreateObjectPropertyForIndividual(nameId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, nameId);

                s_Controller.CreateDatatypePropertyForIndividual(nameId, m_BaseNs + s_Config.DataTypeProperty.hasValueString, m_ObjectModelController.GetName(componentId), Datatypes.String);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all component positions.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddComponentPositions()
        {
            string className = s_Config.Class.PositionCartesian3D;
            string unitName = GetUnitName(m_ObjectModelController.GetPartUnit());

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                string posId = m_GeoNs + className + "-" + Guid.NewGuid().ToString();

                List<string> position = new List<string>(m_ObjectModelController.GetPosition(componentId));

                s_Controller.CreateIndividual(posId, m_GeoNs + className);

                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, posId);
                s_Controller.CreateObjectPropertyForIndividual(posId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);

                s_Controller.CreateObjectPropertyForIndividual(posId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, posId);

                s_Controller.CreateDatatypePropertyForIndividual(posId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateX, position[0], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(posId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateY, position[1], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(posId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateZ, position[2], Datatypes.Float);

                s_Controller.CreateObjectPropertyForIndividual(posId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + unitName);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds 'Bounding Box' coordinates of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddRelationalBoundingBoxes()
        {
            if (!m_ChildComponentsAdded) throw new NotSupportedException();

            List<string> componentsWithoutBoundingBox = new List<string>();

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                IEnumerable<string> bounding_box = m_ObjectModelController.GetBoundingBox(componentId);

                if (bounding_box.Count() == 6)
                    addBoundingBox(new List<string>(bounding_box), null, componentId);
                else componentsWithoutBoundingBox.Add(componentId);
            }

            Console.WriteLine();

            foreach (string componentId in componentsWithoutBoundingBox)
                addBoundingBox(null, getBoundingBox(componentId), componentId);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds hole features of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddHoleFeatures()
        {
            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                foreach (string holeId in m_ObjectModelController.GetHoleFeatures(componentId))
                {
                    s_Controller.CreateIndividual(m_GeoNs + holeId, m_GeoNs + s_Config.Class.HoleFeature);
                    s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasSubEntity, m_GeoNs + holeId);
                    s_Controller.CreateObjectPropertyForIndividual(m_GeoNs + holeId, m_BaseNs + s_Config.ObjectProperty.hasSuperEntity, m_BaseNs + componentId);

                    m_HoleFeaturesAdded = true;
                }
            }

            if (!m_HoleFeaturesAdded) throw new NotSupportedException();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds the location of hole.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddHolePositions()
        {
            if (!m_HoleFeaturesAdded) throw new NotSupportedException();

            string className = s_Config.Class.PositionCartesian3D;
            string unitName = GetUnitName(m_ObjectModelController.GetPartUnit());

            foreach (string holeId in m_ObjectModelController.GetHoleFeatures())
            {
                List<string> holePosition = new List<string>(m_ObjectModelController.GetHolePosition(holeId));

                string holePosId = m_GeoNs + className + "-" + Guid.NewGuid().ToString();

                s_Controller.CreateIndividual(holePosId, m_GeoNs + className);

                s_Controller.CreateObjectPropertyForIndividual(m_GeoNs + holeId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, holePosId);
                s_Controller.CreateObjectPropertyForIndividual(holePosId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_GeoNs + holeId);

                s_Controller.CreateObjectPropertyForIndividual(holePosId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, holePosId);

                s_Controller.CreateDatatypePropertyForIndividual(holePosId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateX, holePosition[0], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(holePosId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateY, holePosition[1], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(holePosId, m_GeoNs + s_Config.DataTypeProperty.hasCoordinateZ, holePosition[2], Datatypes.Float);

                s_Controller.CreateObjectPropertyForIndividual(holePosId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + unitName);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds the direction of every hole.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddHoleDirections()
        {
            if (!m_HoleFeaturesAdded) throw new NotSupportedException();

            string className = s_Config.Class.Direction;

            foreach (string HoleId in m_ObjectModelController.GetHoleFeatures())
            {
                List<string> holeDirectionVector = new List<string>(m_ObjectModelController.GetHoleDirection(HoleId));

                string holeDirectionId = m_GeoNs + className + "-" + Guid.NewGuid();

                s_Controller.CreateIndividual(holeDirectionId, m_GeoNs + className);

                s_Controller.CreateObjectPropertyForIndividual(m_GeoNs + HoleId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, holeDirectionId);
                s_Controller.CreateObjectPropertyForIndividual(holeDirectionId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_GeoNs + HoleId);

                s_Controller.CreateObjectPropertyForIndividual(holeDirectionId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, holeDirectionId);

                s_Controller.CreateDatatypePropertyForIndividual(holeDirectionId, m_GeoNs + s_Config.DataTypeProperty.hasDirectionX, holeDirectionVector[0], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(holeDirectionId, m_GeoNs + s_Config.DataTypeProperty.hasDirectionY, holeDirectionVector[1], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(holeDirectionId, m_GeoNs + s_Config.DataTypeProperty.hasDirectionZ, holeDirectionVector[2], Datatypes.Float);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all hole diameters.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddHoleExpressions()
        {
            if (!m_HoleFeaturesAdded) throw new NotSupportedException();

            string className = s_Config.Class.HoleExpression;

            foreach (string holeId in m_ObjectModelController.GetHoleFeatures())
            {
                var holeExpressions = m_ObjectModelController.GetHoleExpressions(holeId);

                foreach (string[] arrExpression in holeExpressions)
                {
                    string holeExpressionId = m_GeoNs + className + "-" + Guid.NewGuid();

                    s_Controller.CreateIndividual(holeExpressionId, m_GeoNs + className);

                    s_Controller.CreateObjectPropertyForIndividual(m_GeoNs + holeId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, holeExpressionId);
                    s_Controller.CreateObjectPropertyForIndividual(holeExpressionId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_GeoNs + holeId);

                    s_Controller.CreateDatatypePropertyForIndividual(holeExpressionId, m_BaseNs + s_Config.DataTypeProperty.hasValueString, arrExpression[0], Datatypes.String);
                    s_Controller.CreateDatatypePropertyForIndividual(holeExpressionId, m_BaseNs + s_Config.DataTypeProperty.hasValueFloat, arrExpression[1], Datatypes.Float);
                    s_Controller.CreateObjectPropertyForIndividual(holeExpressionId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + GetUnitName(arrExpression[2]));
                }
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Add all joints (identifier). Required for kinematics extraction.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddJoints()
        {
            foreach (string jointId in m_ObjectModelController.GetJoints())
            {
                s_Controller.CreateIndividual(m_DynNs + jointId, m_DynNs + s_Config.Class.Joint);

                foreach (string linkId in m_ObjectModelController.GetLinksOfJoint(jointId))
                {
                    s_Controller.CreateObjectPropertyForIndividual(m_DynNs + jointId, m_BaseNs + s_Config.ObjectProperty.hasSubAttribute, m_DynNs + linkId);
                    s_Controller.CreateObjectPropertyForIndividual(m_DynNs + linkId, m_BaseNs + s_Config.ObjectProperty.hasSuperAttribute, m_DynNs + jointId);
                }
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Add all links (identifier). Required for kinematics extraction.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddLinks()
        {
            foreach (string linkId in m_ObjectModelController.GetLinks())
            {
                s_Controller.CreateIndividual(m_DynNs + linkId, m_DynNs + s_Config.Class.Link);

                foreach (string componentId in m_ObjectModelController.GetComponentsOfLink(linkId))
                {
                    s_Controller.CreateObjectPropertyForIndividual(m_DynNs + linkId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);
                    s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, m_DynNs + linkId);
                }
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds component mass
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddMassOfComponents()
        {
            if (!m_ChildComponentsAdded) throw new NotSupportedException();
            
            Dictionary<string, double> componentsWithIncompleteMass = new Dictionary<string, double>();
            string unit = "";

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                var massInfo = m_ObjectModelController.GetMassOfComponent(componentId);
                double mass = double.Parse(massInfo[0], NumberStyles.Float, CultureInfo.CurrentCulture);
                bool incomplete = bool.Parse(massInfo[1]);
                unit = GetUnitName(massInfo[2]);

                if (incomplete) componentsWithIncompleteMass.Add(componentId, mass);
                else addMassOfComponents(componentId, mass.ToString(), unit);
            }

            foreach (KeyValuePair<string,double> componentAndMass in componentsWithIncompleteMass)
            {
                double assembledMass = getMassOfComponent(componentAndMass.Key, componentAndMass.Value);
                addMassOfComponents(componentAndMass.Key, assembledMass.ToString(), unit);
            }
        }
        
        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds mass moment of inertia (WCS)
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddMomentsOfInertiaInMassCenter()
        {
            string className = s_Config.Class.MomentOfInertiaInMassCenter;

            foreach (string componentId in m_ObjectModelController.GetAllComponents())
            {
                var inertia = m_ObjectModelController.GetMomentsOfInertia(componentId);

                // Skip if all values are 0
                if (inertia.Key.All(value => value == "0")) continue;

                string inertiaId = m_DynNs + className + "-" + Guid.NewGuid();

                s_Controller.CreateIndividual(inertiaId, m_DynNs + className);

                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, inertiaId);
                s_Controller.CreateObjectPropertyForIndividual(inertiaId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);

                s_Controller.CreateDatatypePropertyForIndividual(inertiaId, m_DynNs + s_Config.DataTypeProperty.hasInertiaMassCenterX, inertia.Key[0], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(inertiaId, m_DynNs + s_Config.DataTypeProperty.hasInertiaMassCenterY, inertia.Key[1], Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(inertiaId, m_DynNs + s_Config.DataTypeProperty.hasInertiaMassCenterZ, inertia.Key[2], Datatypes.Float);

                s_Controller.CreateObjectPropertyForIndividual(inertiaId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + GetUnitName(inertia.Value));
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all mechanical connections between components realized by a 
        /// mechanical connector.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void AddMechanicalConnections()
        {
            string className = s_Config.Class.MechanicalConnection;

            var connectors = m_ObjectModelController.GetAllComponentConnectors();

            foreach (var componentId in connectors)
            {
                string connectionId = m_AssemNs + className + "-" + Guid.NewGuid();

                s_Controller.CreateIndividual(connectionId, m_AssemNs + className);

                s_Controller.CreateObjectPropertyForIndividual(connectionId, m_BaseNs + s_Config.ObjectProperty.isRealizedBy, m_BaseNs + componentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.isRealizationOf, connectionId);

                s_Controller.CreateObjectPropertyForIndividual(connectionId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, connectionId);

                var connectedComps = m_ObjectModelController.GetComponentsConnectedWithConnector(componentId);

                foreach (var connectedCompId in connectedComps)
                {
                    s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + connectedCompId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, connectionId);
                    s_Controller.CreateObjectPropertyForIndividual(connectionId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + connectedCompId);
                }
            }
        }
        
        #endregion

        //========================================================================================

        #region Helper methods

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Helper method for 'AddBoundingBox'
        /// </summary>
        /// <param name="b">Bounding box coordinates as string</param>
        /// <param name="b2">Bounding box coordinates as double</param>
        /// <param name="componentId">The component identifier</param>
        //-------------------------------------------------------------------------//
        private void addBoundingBox(List<string> b, List<double> b2, string componentId)
        {
            string className = s_Config.Class.RelationalBoundingBox;
            string unitName = GetUnitName(m_ObjectModelController.GetPartUnit());

            if (b == null && b2 != null)
                b = b2.Select(coord => coord.ToString(CultureInfo.CurrentCulture)).ToList();

            if (b != null)
            {
                string boxId = m_GeoNs + className + "-" + Guid.NewGuid().ToString();

                s_Controller.CreateIndividual(boxId, m_GeoNs + className);

                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, boxId);
                s_Controller.CreateObjectPropertyForIndividual(boxId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);

                s_Controller.CreateObjectPropertyForIndividual(boxId, m_BaseNs + s_Config.ObjectProperty.hasReferenceTo, m_BaseNs + m_RootComponentId);
                s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + m_RootComponentId, m_BaseNs + s_Config.ObjectProperty.isReferenceOf, boxId);

                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMinimumCoordinateX, b[0].ToString(), Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMinimumCoordinateY, b[1].ToString(), Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMinimumCoordinateZ, b[2].ToString(), Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMaximumCoordinateX, b[3].ToString(), Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMaximumCoordinateY, b[4].ToString(), Datatypes.Float);
                s_Controller.CreateDatatypePropertyForIndividual(boxId, m_GeoNs + s_Config.DataTypeProperty.hasMaximumCoordinateZ, b[5].ToString(), Datatypes.Float);

                s_Controller.CreateObjectPropertyForIndividual(boxId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + unitName);
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Helper methode for 'AddMassOfComponents'. Add mass information about 
        /// components to the ontology database.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <param name="mass">The mass value</param>
        //-------------------------------------------------------------------------//
        private void addMassOfComponents(string componentId, string mass, string unit)
        {
            Console.Write(".");
            string className = s_Config.Class.Mass;
            string massId = m_DynNs + className + "-" + Guid.NewGuid();

            s_Controller.CreateIndividual(massId, m_DynNs + className);
            s_Controller.CreateObjectPropertyForIndividual(m_BaseNs + componentId, m_BaseNs + s_Config.ObjectProperty.hasAttribute, massId);
            s_Controller.CreateObjectPropertyForIndividual(massId, m_BaseNs + s_Config.ObjectProperty.isAttributeOf, m_BaseNs + componentId);
            s_Controller.CreateDatatypePropertyForIndividual(massId, m_BaseNs + s_Config.DataTypeProperty.hasValueFloat, mass.ToString(CultureInfo.CurrentCulture), Datatypes.Float);

            s_Controller.CreateObjectPropertyForIndividual(massId, m_BaseNs + s_Config.ObjectProperty.hasUnit, m_UnitsNs + unit);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the mass of a given component.
        /// Also computes the mass of an assembled component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A float value as string</returns>
        //-------------------------------------------------------------------------//
        private float getMassOfComponent(string componentId, double incompleteMass)
        {
            float massOfComponent = 0.0f;

            // Insert base namespace to componentId
            if (!componentId.Contains("#"))
                componentId = s_Config.BaseNamespace + "#" + componentId;

            // Ask mass of component
            string massQuery = "SELECT ?attribute " +
                               "WHERE { " + s_Controller.ChangeFrontNamespace(componentId) + " " + m_BaseNsShort + ":" + s_Config.ObjectProperty.hasAttribute + " ?attribute ." +
                                       "?attribute " + m_RdfNsShort + ":type " + m_DynNsShort + ":" + s_Config.Class.Mass + " }";

            string massId = s_Controller.Execute(massQuery).FirstOrDefault();

            if (string.IsNullOrEmpty(massId))
            {
                // If the current component has no mass information, get the sum of the masses of its child components
                List<string> childComponents = m_ObjectModelController.GetChildComponents(componentId.Split('#')[1]).ToList();

                // Compute the mass recursively
                foreach (string childComponent in childComponents)
                    massOfComponent += getMassOfComponent(childComponent, default(double));
            }
            else
            {
                // Get mass value
                string massValue = s_Controller.GetIndividualPropertyValue(massId, m_BaseNs + s_Config.DataTypeProperty.hasValueFloat, false).FirstOrDefault();

                // Crop the mass from the query string
                string mass = massValue.Split('^')[0].Trim('"');

                // Parse numbers with decimal point and/or exponent
                if (!float.TryParse(mass, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out massOfComponent))
                    massOfComponent = float.Parse(mass, NumberStyles.Float, CultureInfo.InvariantCulture);
            }

            return massOfComponent + (float)incompleteMass;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the bounding box of a given component.
        /// Also computes the bounding box of an assembled component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of coordinates (X_min, Y_min, Z_min, X_max, Y_max, Z_max)</returns>
        //-------------------------------------------------------------------------//
        private List<double> getBoundingBox(string componentId)
        {
            // Insert base namespace to componentId
            if (!componentId.Contains("#"))
                componentId = s_Config.BaseNamespace + "#" + componentId;

            // Ask bounding box of component
            string boxQuery = "SELECT ?attribute " +
                              "WHERE { " + s_Controller.ChangeFrontNamespace(componentId) + " " + m_BaseNsShort + ":" + s_Config.ObjectProperty.hasAttribute + " ?attribute ." +
                                      "?attribute " + m_RdfNsShort + ":type " + m_GeoNsShort + ":" + s_Config.Class.RelationalBoundingBox + " }";

            string boxId = s_Controller.Execute(boxQuery).FirstOrDefault();

            // If the current component has no bounding box information, 
            // get the minimum and maximum coordinates from its child components
            if (string.IsNullOrEmpty(boxId))
            {
                List<double> boundingBox = new List<double>();

                // Get all child components
                var childComponents = m_ObjectModelController.GetChildComponents(componentId.Split('#')[1]);

                // Get all bounding box coordinates of every child component
                var allCoordinates = childComponents.SelectMany(child => getBoundingBox(child)).ToList();

                if (allCoordinates.Count() >= 6)
                {
                    // Get minimum and maximum coordinates
                    for (int i = 0; i < 6; i++)
                    {
                        List<double> coordinateRange = new List<double>();

                        for (int j = i; j < allCoordinates.Count(); j = j + 6)
                            coordinateRange.Add(allCoordinates[j]);

                        if (i < 3) boundingBox.Add(coordinateRange.Min());
                        else boundingBox.Add(coordinateRange.Max());
                    }

                    return boundingBox;
                }

                // In case of missing bounding box (e.g. every measure of length of component or body is 0.0)
                return new List<double>(new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            }

            // Else return the coordinates of the current components' bounding box
            return getCoordinates(boxId).ToList();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Retrieve bounding box coordinates from database
        /// </summary>
        /// <param name="boxId">The bounding box identifier</param>
        /// <returns>A list of coordinates</returns>
        //-------------------------------------------------------------------------//
        private double[] getCoordinates(string boxId)
        {
            string geoNs = s_Config.GeometryNamespace + "#";
            InformationModel.DataTypeProperties dp = s_Config.DataTypeProperty;

            double minX = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMinimumCoordinateX, false));
            double minY = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMinimumCoordinateY, false));
            double minZ = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMinimumCoordinateZ, false));
            double maxX = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMaximumCoordinateX, false));
            double maxY = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMaximumCoordinateY, false));
            double maxZ = parseCoordinate(s_Controller.GetIndividualPropertyValue(boxId, geoNs + dp.hasMaximumCoordinateZ, false));

            return new double[] { minX, minY, minZ, maxX, maxY, maxZ };
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Parse a query string as double
        /// </summary>
        /// <param name="queryResult">A list of strings (the query result)</param>
        /// <returns>A double value</returns>
        //-------------------------------------------------------------------------//
        private static double parseCoordinate(List<string> queryResult)
        {
            string coord = queryResult[0].Split('^')[0].Trim('"');

            return double.Parse(coord, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        #endregion

        //========================================================================================

        #region Create information model

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Cycles through all methods reflected from a given object with properties
        /// </summary>
        //-------------------------------------------------------------------------//
        private void cycleMethods(object obj)
        {
            List<string> pendingMethods = obj as List<string>;

            if (pendingMethods == null)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
                {
                    if (prop.Name.Contains("Method"))
                    {
                        string method = prop.GetValue(obj).ToString();

                        Console.ResetColor();
                        Console.Write(method.Remove(0, 3));

                        try
                        {
                            invoke(method);
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("[PENDING]\n");
                            m_PendingMethods.Add(method);
                        }
                    }
                }
            }
            else
            {
                foreach (string method in pendingMethods)
                {
                    Console.ResetColor();
                    Console.Write(method.Remove(0, 3));

                    try
                    {
                        invoke(method);
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[N/A]\n");
                        Console.ResetColor();
                    }
                }
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all Add* methods reflected from a given object with properties
        /// </summary>
        //-------------------------------------------------------------------------//
        private List<string> getMethods(object obj)
        {
            List<string> addMethods = new List<string>();

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
                if (prop.Name.Contains("Method"))
                    addMethods.Add(prop.GetValue(obj).ToString());

            return addMethods;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all available Add* methods.
        /// </summary>
        //-------------------------------------------------------------------------//
        public List<string> GetAllMethods()
        {
            List<string> allAddMethods = new List<string>(getMethods(s_Config.Class));
            allAddMethods.AddRange(getMethods(s_Config.DataTypeProperty));
            allAddMethods.AddRange(getMethods(s_Config.ObjectProperty));

            return allAddMethods;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Invokes a method which equals the string 'method'.
        /// </summary>
        //-------------------------------------------------------------------------//
        private void invoke(string method)
        {
            GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, this, null, CultureInfo.CurrentCulture);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK]\n");
            Console.ResetColor();
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void BeginExtraction()
        {
            cycleMethods(s_Config.Class);
            cycleMethods(s_Config.DataTypeProperty);
            cycleMethods(s_Config.ObjectProperty);
            cycleMethods(m_PendingMethods);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void BeginExtraction(List<string> methodList)
        {
            m_Index = 0;
            m_Count = 1;

            cycleMethods(methodList);

            m_Index = -1;
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        public void BeginExtraction(string method)
        {
            m_Index = 0;
            m_Count = 1;

            List<string> methodList = new List<string>();
            methodList.Add(method);

            BeginExtraction(methodList);
        }

        #endregion

        //========================================================================================
    }
}