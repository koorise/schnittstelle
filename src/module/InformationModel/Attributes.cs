/*! \file Attributes.cs
 *  \brief Custom OWL class attributes
 * 
 *  Since there is only one generic object protperty for individuals in our information model
 *  (hasAttribute), this class contains all property names, which are needed to invoke the 
 *  extraction methods.
 *  This class doen't contain any definitions for our information model.
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
    public class Attributes
    {
        //========================================================================================

        #region Fields

        private string
            m_Method_Joint,
            m_Method_Link,
            m_Method_PositionCartesian3D_PhysicalComponent,
            m_Method_PositionCartesian3D_HoleFeature,
            m_Method_Direction_HoleFeature;

        #endregion

        //========================================================================================

        #region Properties
        
        // Joint Attribute

        [Configuration("Attributes\\Joint\\Method", "AddJointAttributes")]
        public string Method_Joint
        {
            get { return m_Method_Joint; }
            set { m_Method_Joint = value; }
        }

        // Link Attribute

        [Configuration("Attributes\\Link\\Method", "AddLinkAttributes")]
        public string Method_Link
        {
            get { return m_Method_Link; }
            set { m_Method_Link = value; }
        }

        // PositionCartesian3D_PhysicalComponent Attribute

        [Configuration("Attributes\\PositionCartesian3D\\PhysicalComponent\\Method", "AddComponentPositions")]
        public string Method_PositionCartesian3D_PhysicalComponent
        {
            get { return m_Method_PositionCartesian3D_PhysicalComponent; }
            set { m_Method_PositionCartesian3D_PhysicalComponent = value; }
        }

        // PositionCartesian3D_HoleFeature Attribute

        [Configuration("Attributes\\PositionCartesian3D\\HoleFeature\\Method", "AddHolePositions")]
        public string Method_PositionCartesian3D_HoleFeature
        {
            get { return m_Method_PositionCartesian3D_HoleFeature; }
            set { m_Method_PositionCartesian3D_HoleFeature = value; }
        }

        // Direction_HoleFeature Attribute

        [Configuration("Attributes\\Direction\\HoleFeature\\Method", "AddHoleDirections")]
        public string Method_Direction_HoleFeature
        {
            get { return m_Method_Direction_HoleFeature; }
            set { m_Method_Direction_HoleFeature = value; }
        }

        #endregion

        //========================================================================================
    }
}
