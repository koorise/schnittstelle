/*! \file DatatypeProperties.cs
 *  \brief OWL datatype property definitions
 *  
 *  This class contains all datatype property names as defined in our information model.
 * 
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		04/2010
 */

using Siemens.HCSS.Configuration;

namespace Schnittstelle_NX.InformationModel
{
    public class DataTypeProperties
    {
        //========================================================================================

        #region Fields

        // OWL Names
        private string
            m_hasCoordinateX, 
            m_hasCoordinateY, 
            m_hasCoordinateZ,
            m_hasDirectionX, 
            m_hasDirectionY, 
            m_hasDirectionZ,
            m_hasMinimumCoordinateX, 
            m_hasMinimumCoordinateY, 
            m_hasMinimumCoordinateZ,
            m_hasMaximumCoordinateX, 
            m_hasMaximumCoordinateY, 
            m_hasMaximumCoordinateZ,
            m_hasValueFloat,
            m_hasValueString,
            m_hasInertiaMassCenterX,
            m_hasInertiaMassCenterY,
            m_hasInertiaMassCenterZ;

        #endregion

        //========================================================================================

        #region Properties

        // hasValueFloat Property

        [Configuration("hasValueFloat\\OWLName", "hasValueFloat")]
        public string hasValueFloat
        {
            get { return m_hasValueFloat; }
            set { m_hasValueFloat = value; }
        }

        // hasValueString Property

        [Configuration("hasValueString\\OWLName", "hasValueString")]
        public string hasValueString
        {
            get { return m_hasValueString; }
            set { m_hasValueString = value; }
        }

        // hasCoordinateX Property

        [Configuration("hasCoordinateX\\OWLName", "hasCoordinateX")]
        public string hasCoordinateX
        {
            get { return m_hasCoordinateX; }
            set { m_hasCoordinateX = value; }
        }

        // hasCoordinateY Property

        [Configuration("hasCoordinateY\\OWLName", "hasCoordinateY")]
        public string hasCoordinateY
        {
            get { return m_hasCoordinateY; }
            set { m_hasCoordinateY = value; }
        }

        // hasCoordinateZ Property

        [Configuration("hasCoordinateZ\\OWLName", "hasCoordinateZ")]
        public string hasCoordinateZ
        {
            get { return m_hasCoordinateZ; }
            set { m_hasCoordinateZ = value; }
        }

        // hasDirectionX Property

        [Configuration("hasDirectionX\\OWLName", "hasDirectionX")]
        public string hasDirectionX
        {
            get { return m_hasDirectionX; }
            set { m_hasDirectionX = value; }
        }

        // hasDirectionY Property

        [Configuration("hasDirectionY\\OWLName", "hasDirectionY")]
        public string hasDirectionY
        {
            get { return m_hasDirectionY; }
            set { m_hasDirectionY = value; }
        }

        // hasDirectionZ Property

        [Configuration("hasDirectionZ\\OWLName", "hasDirectionZ")]
        public string hasDirectionZ
        {
            get { return m_hasDirectionZ; }
            set { m_hasDirectionZ = value; }
        }

        // hasMinimumXCoordinate Property

        [Configuration("hasMinimumCoordinateX\\OWLName", "hasMinimumCoordinateX")]
        public string hasMinimumCoordinateX
        {
            get { return m_hasMinimumCoordinateX; }
            set { m_hasMinimumCoordinateX = value; }
        }

        // hasMinimumCoordinateY Property

        [Configuration("hasMinimumCoordinateY\\OWLName", "hasMinimumCoordinateY")]
        public string hasMinimumCoordinateY
        {
            get { return m_hasMinimumCoordinateY; }
            set { m_hasMinimumCoordinateY = value; }
        }

        // hasMinimumCoordinateZ Property

        [Configuration("hasMinimumCoordinateZ\\OWLName", "hasMinimumCoordinateZ")]
        public string hasMinimumCoordinateZ
        {
            get { return m_hasMinimumCoordinateZ; }
            set { m_hasMinimumCoordinateZ = value; }
        }

        // hasMaximumCoordinateX Property

        [Configuration("hasMaximumCoordinateX\\OWLName", "hasMaximumCoordinateX")]
        public string hasMaximumCoordinateX
        {
            get { return m_hasMaximumCoordinateX; }
            set { m_hasMaximumCoordinateX = value; }
        }

        // hasMaximumCoordinateY Property

        [Configuration("hasMaximumCoordinateY\\OWLName", "hasMaximumCoordinateY")]
        public string hasMaximumCoordinateY
        {
            get { return m_hasMaximumCoordinateY; }
            set { m_hasMaximumCoordinateY = value; }
        }

        // hasMaximumCoordinateZ Property

        [Configuration("hasMaximumCoordinateZ\\OWLName", "hasMaximumCoordinateZ")]
        public string hasMaximumCoordinateZ
        {
            get { return m_hasMaximumCoordinateZ; }
            set { m_hasMaximumCoordinateZ = value; }
        }

        // hasInertiaMassCenterX Property

        [Configuration("hasInertiaMassCenterX\\OWLName", "hasInertiaMassCenterX")]
        public string hasInertiaMassCenterX
        {
            get { return m_hasInertiaMassCenterX; }
            set { m_hasInertiaMassCenterX = value; }
        }

        // hasInertiaMassCenterY Property

        [Configuration("hasInertiaMassCenterY\\OWLName", "hasInertiaMassCenterY")]
        public string hasInertiaMassCenterY
        {
            get { return m_hasInertiaMassCenterY; }
            set { m_hasInertiaMassCenterY = value; }
        }

        // hasInertiaMassCenterZ Property

        [Configuration("hasInertiaMassCenterZ\\OWLName", "hasInertiaMassCenterZ")]
        public string hasInertiaMassCenterZ
        {
            get { return m_hasInertiaMassCenterZ; }
            set { m_hasInertiaMassCenterZ = value; }
        }

        #endregion

        //========================================================================================
    }
}