/*! \file ObjectModelAccessImpl.cs
 *  \brief
 *  
 *
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		06/2010
 */

using System.Collections.Generic;
using System.Linq;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;
using System;


namespace deleteGuids
{
    public abstract class ObjectModelAccessImpl
    {
        //****************************************************************************************

        #region Fields

        // The NX session
        protected Session m_Session;
        protected UFSession m_UFSession;

        // The NX root part
        protected Part m_RootPart;

        // NX save/load status fields
        protected PartLoadStatus m_PartLoadStatus;
        protected PartSaveStatus m_PartSaveStatus;

        // The part and simulation file path
        protected string m_PartPath, m_MotionSimName;

        // GUID dictionaries
        private Dictionary<Component, string> m_ComponentIdDictionary;

        #endregion

        //****************************************************************************************

        #region Properties

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get or set the full NX part file path
        /// </summary>
        //-------------------------------------------------------------------------//
        public string PartPath
        {
            get { return m_PartPath; }
            set { m_PartPath = value; }
        }

        #endregion Properties

        //****************************************************************************************

        #region File Processing

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Save changes to NX part or simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public abstract void Save();

        #endregion

        //****************************************************************************************

        #region public methods
        

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
        /// Deletes a string attribute by title (of a given component).
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <param name="title">The attribute title</param>
        //-------------------------------------------------------------------------//
        public void DeleteStringAttribute(string componentId, string title)
        {
            Component c = getComponentById(componentId);

            try
            {
                c.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, title);
            }
            catch (Exception)
            { 
                throw new Exception("Object has no attribute with title \"" + title + "\"!"); 
            }
        }

        #endregion

        //****************************************************************************************

        #region private get* methods

        #region get*

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
        private IEnumerable<Component> getAllComponents()
        {
            Component root = getRootComponent();

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

        #region get* By Id

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get a component by its GUID.
        /// </summary>
        /// <param name="id">The component identifier</param>
        /// <returns>A NX Component object</returns>
        //-------------------------------------------------------------------------//
        private Component getComponentById(string id)
        {
            return m_ComponentIdDictionary.Where(comp => comp.Value == id).FirstOrDefault().Key;
        }

        #endregion

        #endregion

        //****************************************************************************************
    }
}