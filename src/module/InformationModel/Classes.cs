/*! \file Classes.cs
 *  \brief OWL class defintions
 *  
 *  This class contains all OWL class names as defined in our information model.
 *  It also contains all corresponding extraction methods (except for position 
 *  and direction class)
 * 
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		05/2010
 */

using Siemens.HCSS.Configuration;

namespace Schnittstelle_NX.InformationModel
{
    public class Classes
    {
        //========================================================================================

        #region Fields

        // OWL Names

        private string
            m_PhysicalComponent,
            m_Link,
            m_Joint,
            m_RelationalBoundingBox,
            m_HoleFeature,
            m_HoleExpression,
            m_MomentOfInertiaInMassCenter,
            m_PositionCartesian3D,
            m_Direction,
            m_Mass,
            m_NameInAssembly,
            m_TipAngle,
            m_Diameter,
            m_Depth,
            m_MechanicalConnection;

        // Corresponding Methods

        private string
            m_Method_PhysicalComponent,
            m_Method_Link,
            m_Method_Joint,
            m_Method_RelationalBoundingBox,
            m_Method_HoleFeature,
            m_Method_HoleExpression,
            m_Method_MomentOfInertiaInMassCenter,
            m_Method_PositionCartesian3D_PhysicalComponent,
            m_Method_PositionCartesian3D_HoleFeature,
            m_Method_Direction_HoleFeature,
            m_Method_Mass,
            m_Method_NameInAssembly,
            m_Method_MechanicalConnection;

        #endregion

        //========================================================================================

        #region Properties

        // PhysicalComponent Class

        [Configuration("PhysicalComponent\\OWLName", "PhysicalComponent")]
        public string PhysicalComponent
        {
            get { return m_PhysicalComponent; }
            set { m_PhysicalComponent = value; }
        }

        [Configuration("PhysicalComponent\\Method", "AddComponents")]
        public string Method_PhysicalComponent
        {
            get { return m_Method_PhysicalComponent; }
            set { m_Method_PhysicalComponent = value; }
        }

        // Link Class

        [Configuration("Link\\OWLName", "Link")]
        public string Link
        {
            get { return m_Link; }
            set { m_Link = value; }
        }

        [Configuration("Link\\Method", "AddLinks")]
        public string Method_Link
        {
            get { return m_Method_Link; }
            set { m_Method_Link = value; }
        }

        // Joint Class

        [Configuration("Joint\\OWLName", "Joint")]
        public string Joint
        {
            get { return m_Joint; }
            set { m_Joint = value; }
        }

        [Configuration("Joint\\Method", "AddJoints")]
        public string Method_Joint
        {
            get { return m_Method_Joint; }
            set { m_Method_Joint = value; }
        }

        // RelationalBoundingBox Class

        [Configuration("RelationalBoundingBox\\OWLName", "RelationalBoundingBox")]
        public string RelationalBoundingBox
        {
            get { return m_RelationalBoundingBox; }
            set { m_RelationalBoundingBox = value; }
        }

        [Configuration("RelationalBoundingBox\\Method", "AddRelationalBoundingBoxes")]
        public string Method_RelationalBoundingBox
        {
            get { return m_Method_RelationalBoundingBox; }
            set { m_Method_RelationalBoundingBox = value; }
        }

        // HoleFeature Class

        [Configuration("HoleFeature\\OWLName", "HoleFeature")]
        public string HoleFeature
        {
            get { return m_HoleFeature; }
            set { m_HoleFeature = value; }
        }

        [Configuration("HoleFeature\\Method", "AddHoleFeatures")]
        public string Method_HoleFeature
        {
            get { return m_Method_HoleFeature; }
            set { m_Method_HoleFeature = value; }
        }

        // HoleExpression Class

        [Configuration("HoleExpression\\OWLName", "HoleExpression")]
        public string HoleExpression
        {
            get { return m_HoleExpression; }
            set { m_HoleExpression = value; }
        }

        [Configuration("HoleExpression\\Method", "AddHoleExpressions")]
        public string Method_HoleExpression
        {
            get { return m_Method_HoleExpression; }
            set { m_Method_HoleExpression = value; }
        }

        // MomentOfInertiaInMassCenter Class

        [Configuration("MomentOfInertiaInMassCenter\\OWLName", "MomentOfInertiaInMassCenter")]
        public string MomentOfInertiaInMassCenter
        {
            get { return m_MomentOfInertiaInMassCenter; }
            set { m_MomentOfInertiaInMassCenter = value; }
        }

        [Configuration("MomentOfInertiaInMassCenter\\Method", "AddMomentsOfInertiaInMassCenter")]
        public string Method_MomentOfInertiaInMassCenter
        {
            get { return m_Method_MomentOfInertiaInMassCenter; }
            set { m_Method_MomentOfInertiaInMassCenter = value; }
        }

        // PositionCartesian3D Class

        [Configuration("PositionCartesian3D\\OWLName", "PositionCartesian3D")]
        public string PositionCartesian3D
        {
            get { return m_PositionCartesian3D; }
            set { m_PositionCartesian3D = value; }
        }
        
        [Configuration("PositionCartesian3D\\PhysicalComponent\\Method", "AddComponentPositions")]
        public string Method_PositionCartesian3D_PhysicalComponent
        {
            get { return m_Method_PositionCartesian3D_PhysicalComponent; }
            set { m_Method_PositionCartesian3D_PhysicalComponent = value; }
        }
                
        [Configuration("PositionCartesian3D\\HoleFeature\\Method", "AddHolePositions")]
        public string Method_PositionCartesian3D_HoleFeature
        {
            get { return m_Method_PositionCartesian3D_HoleFeature; }
            set { m_Method_PositionCartesian3D_HoleFeature = value; }
        }

        // Direction Class

        [Configuration("Direction\\OWLName", "Direction")]
        public string Direction
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }
        
        [Configuration("Direction\\HoleFeature\\Method", "AddHoleDirections")]
        public string Method_Direction_HoleFeature
        {
            get { return m_Method_Direction_HoleFeature; }
            set { m_Method_Direction_HoleFeature = value; }
        }

        // Mass Class

        [Configuration("Mass\\OWLName", "Mass")]
        public string Mass
        {
            get { return m_Mass; }
            set { m_Mass = value; }
        }

        [Configuration("Mass\\Method", "AddMassOfComponents")]
        public string Method_Mass
        {
            get { return m_Method_Mass; }
            set { m_Method_Mass = value; }
        }

        // NameInAssembly Class

        [Configuration("NameInAssembly\\OWLName", "NameInAssembly")]
        public string NameInAssembly
        {
            get { return m_NameInAssembly; }
            set { m_NameInAssembly = value; }
        }

        [Configuration("NameInAssembly\\Method", "AddComponentNames")]
        public string Method_NameInAssembly
        {
            get { return m_Method_NameInAssembly; }
            set { m_Method_NameInAssembly = value; }
        }

        // Diameter Class

        [Configuration("Diameter\\OWLName", "Diameter")]
        public string Diameter
        {
            get { return m_Diameter; }
            set { m_Diameter = value; }
        }

        // Depth Class

        [Configuration("Depth\\OWLName", "Depth")]
        public string Depth
        {
            get { return m_Depth; }
            set { m_Depth = value; }
        }

        // TipAngle Class

        [Configuration("TipAngle\\OWLName", "TipAngle")]
        public string TipAngle
        {
            get { return m_TipAngle; }
            set { m_TipAngle = value; }
        }

        // MechanicalConnection Class

        [Configuration("MechanicalConnection\\OWLName", "MechanicalConnection")]
        public string MechanicalConnection
        {
            get { return m_MechanicalConnection; }
            set { m_MechanicalConnection = value; }
        }

        [Configuration("MechanicalConnection\\Method", "AddMechanicalConnections")]
        public string Method_MechanicalConnection
        {
            get { return m_Method_MechanicalConnection; }
            set { m_Method_MechanicalConnection = value; }
        }

        #endregion

        //========================================================================================
    }
}