/*! \file Config.cs
 *  \brief Access to the configuration file.
 *
 *  This class allows easy access to the configuration file, which contains all definitions of
 *  our information model. It contains also all namespaces and owl file names/paths.
 *  The configuration file will be created once with default values, if it does not exist yet.
 *  Otherwise, the values of an existing (compatible) configuration file will be loaded.
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		08/2010
 */

using System;
using Siemens.HCSS.Configuration;


namespace Schnittstelle_NX
{
    public sealed class Config
    {
        //========================================================================================

        #region Fields

        // 'm_Config' is the main key in the configuration file
        private static readonly IConfigurationKey s_Config = ConfigurationManager.GetConfiguration();

        // These are sub keys in the configuration file
        private static IConfigurationKey
            s_KeyInformationModel,
            s_SubKeyClasses,
            s_SubKeyDataTypeProperties,
            s_SubKeyObjectProperties,
            s_SubKeyAttributes,
            s_SubKeyUnits;

        // These objects represent the ontology concept
        private InformationModel.Classes m_Class;
        private InformationModel.DataTypeProperties m_DataTypeProperty;
        private InformationModel.ObjectProperties m_ObjectProperty;
        private InformationModel.Units m_Units;

        // The namespaces required in our information model
        private string m_BaseNs, m_GeoNs, m_DynNs, m_RdfNs, m_UnitsNs, m_AssemNs;

        // The OWL files
        private string
            m_DefaultOwlDir,
            m_DefaultExportDir;

        #endregion

        //========================================================================================

        #region Construction / Destruction

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Creates or loads the configuration file, which contains the ontology concept.
        /// </summary>
        //-------------------------------------------------------------------------//
        public Config()
        {
            m_Class = new InformationModel.Classes();
            m_DataTypeProperty = new InformationModel.DataTypeProperties();
            m_ObjectProperty = new InformationModel.ObjectProperties();
            m_Units = new Schnittstelle_NX.InformationModel.Units();

            s_KeyInformationModel = s_Config.OpenSubKey("InformationModel", true);
            s_SubKeyClasses = s_KeyInformationModel.OpenSubKey("Classes", true);
            s_SubKeyDataTypeProperties = s_KeyInformationModel.OpenSubKey("DatatypeProperties", true);
            s_SubKeyObjectProperties = s_KeyInformationModel.OpenSubKey("ObjectProperties", true);
            s_SubKeyAttributes = s_KeyInformationModel.OpenSubKey("Attributes", true);
            s_SubKeyUnits = s_KeyInformationModel.OpenSubKey("Units", true);

            ConfigurationManager.AssignProperties(s_Config, this);
            ConfigurationManager.AssignProperties(s_SubKeyClasses, m_Class);
            ConfigurationManager.AssignProperties(s_SubKeyDataTypeProperties, m_DataTypeProperty);
            ConfigurationManager.AssignProperties(s_SubKeyObjectProperties, m_ObjectProperty);
            ConfigurationManager.AssignProperties(s_SubKeyUnits, m_Units);
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Writes any changes to the configuration file, whenever a Config object
        /// is disposed.
        /// </summary>
        //-------------------------------------------------------------------------//
        ~Config()
        {
            WriteConfiguration();
        }

        #endregion

        //========================================================================================

        #region Properties

        #region Information Model

        //-------------------------------------------------------------------------//
        /// <summary>
        /// All NX specific OWL classes with corresponding method names.
        /// </summary>
        //-------------------------------------------------------------------------//
        public InformationModel.Classes Class
        {
            get { return m_Class; }
            set { m_Class = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// All NX specific OWL datatype properties with corresponding method names.
        /// </summary>
        //-------------------------------------------------------------------------//
        public InformationModel.DataTypeProperties DataTypeProperty
        {
            get { return m_DataTypeProperty; }
            set { m_DataTypeProperty = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// All NX specific OWL object properties with corresponding method names.
        /// </summary>
        //-------------------------------------------------------------------------//
        public InformationModel.ObjectProperties ObjectProperty
        {
            get { return m_ObjectProperty; }
            set { m_ObjectProperty = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// All units required for attributes.
        /// </summary>
        //-------------------------------------------------------------------------//
        public InformationModel.Units Unit
        {
            get { return m_Units; }
            set { m_Units = value; }
        }

        #endregion
        
        #region Namespaces
        
        //-------------------------------------------------------------------------//
        /// <summary>
        /// Namespace for base.owl
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Base", "http://localhost/ontologies/base.owl")]
        public string BaseNamespace
        {
            get { return m_BaseNs; }
            set { m_BaseNs = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Namespace for geometry.owl
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Geometry", "http://localhost/ontologies/geometry.owl")]
        public string GeometryNamespace
        {
            get { return m_GeoNs; }
            set { m_GeoNs = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Namespace for dynamics.owl
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Dynamics", "http://localhost/ontologies/dynamics.owl")]
        public string DynamicsNamespace
        {
            get { return m_DynNs; }
            set { m_DynNs = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// The default RDF namespace.
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns")]
        public string RdfNamespace
        {
            get { return m_RdfNs; }
            set { m_RdfNs = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Namespace for untis.owl
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Units", "http://sweet.jpl.nasa.gov/1.1/units.owl")]
        public string UnitsNamespace
        {
            get { return m_UnitsNs; }
            set { m_UnitsNs = value; }
        }
             
        //-------------------------------------------------------------------------//
        /// <summary>
        /// Namespace for assembly.owl
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Namespaces\\Assembly", "http://localhost/ontologies/assembly.owl")]
        public string AssemblyNamespace
        {
            get { return m_AssemNs; }
            set { m_AssemNs = value; }
        }
                
        #endregion

        #region Folders

        //-------------------------------------------------------------------------//
        /// <summary>
        /// The directory, where all owl files are stored.
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Files\\DefaultOwlFilePath", @"Informationsmodell")]
        public string DefaultOwlDirectory
        {
            get { return m_DefaultOwlDir; }
            set { m_DefaultOwlDir = value; }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// The directory, where new owl files shall be saved.
        /// </summary>
        //-------------------------------------------------------------------------//
        [Configuration("Files\\DefaultExportFilePath", "Export")]
        public string DefaultExportDirectory
        {
            get { return m_DefaultExportDir; }
            set { m_DefaultExportDir = value; }
        }

        #endregion
        
        #endregion

        //========================================================================================

        #region Methods

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Write or update the configutation file
        /// </summary>
        //-------------------------------------------------------------------------//
        public void WriteConfiguration()
        {
            ConfigurationManager.FlushProperties(s_Config, this);
            ConfigurationManager.WriteConfiguration();
        }

        #endregion

        //========================================================================================
    }
}