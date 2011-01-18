/*! \file ObjectProperties.cs
 *  \brief OWL object property definitions
 *  
 *  This class contains all object property names as defined in our information model.
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
    public class ObjectProperties
    {
        //========================================================================================

        #region Fields

        // OWL Names

        private string
            m_hasAttribute,
            m_isAttributeOf,
            m_hasSubAttribute,
            m_hasSuperAttribute,
            m_hasReferenceTo,
            m_isReferenceOf,
            m_hasSubEntity,
            m_hasSuperEntity,
            m_hasUnit,
            m_isRealizedBy,
            m_isRealizationOf;

        // Corresponding Methods

        private string
            m_Method_hasSubEntity,
            m_Method_hasSuperEntity;

        #endregion

        //========================================================================================

        #region Properties

        // hasAttribute Propety

        [Configuration("hasAttribute\\OWLName", "hasAttribute")]
        public string hasAttribute
        {
            get { return m_hasAttribute; }
            set { m_hasAttribute = value; }
        }

        // isAttributeOf Propety

        [Configuration("isAttributeOf\\OWLName", "isAttributeOf")]
        public string isAttributeOf
        {
            get { return m_isAttributeOf; }
            set { m_isAttributeOf = value; }
        }

        // hasSubAttribute Propety

        [Configuration("hasSubAttribute\\OWLName", "hasSubAttribute")]
        public string hasSubAttribute
        {
            get { return m_hasSubAttribute; }
            set { m_hasSubAttribute = value; }
        }

        // hasSuperAttribute Propety

        [Configuration("hasSuperAttribute\\OWLName", "hasSuperAttribute")]
        public string hasSuperAttribute
        {
            get { return m_hasSuperAttribute; }
            set { m_hasSuperAttribute = value; }
        }

        // hasReferenceTo Propety

        [Configuration("hasReferenceTo\\OWLName", "hasReferenceTo")]
        public string hasReferenceTo
        {
            get { return m_hasReferenceTo; }
            set { m_hasReferenceTo = value; }
        }

        // isReferenceOf Propety

        [Configuration("isReferenceOf\\OWLName", "isReferenceOf")]
        public string isReferenceOf
        {
            get { return m_isReferenceOf; }
            set { m_isReferenceOf = value; }
        }

        // hasSubEntity Propety

        [Configuration("hasSubEntity\\OWLName", "hasSubEntity")]
        public string hasSubEntity
        {
            get { return m_hasSubEntity; }
            set { m_hasSubEntity = value; }
        }

        [Configuration("hasSubEntity\\Method", "AddChildComponents")]
        public string Method_hasSubEntity
        {
            get { return m_Method_hasSubEntity; }
            set { m_Method_hasSubEntity = value; }
        }

        // hasSuperEntity Property

        [Configuration("hasSuperEntity\\OWLName", "hasSuperEntity")]
        public string hasSuperEntity
        {
            get { return m_hasSuperEntity; }
            set { m_hasSuperEntity = value; }
        }

        [Configuration("hasSuperEntity\\Method", "AddParentComponents")]
        public string Method_hasSuperEntity
        {
            get { return m_Method_hasSuperEntity; }
            set { m_Method_hasSuperEntity = value; }
        }

        // hasUnit Property

        [Configuration("hasUnit\\OWLName", "hasUnit")]
        public string hasUnit
        {
            get { return m_hasUnit; }
            set { m_hasUnit = value; }
        }

        // isRealizedBy Property

        [Configuration("isRealizedBy\\OWLName", "isRealizedBy")]
        public string isRealizedBy
        {
            get { return m_isRealizedBy; }
            set { m_isRealizedBy = value; }
        }

        // isRealizationOf Property

        [Configuration("isRealizationOf\\OWLName", "isRealizationOf")]
        public string isRealizationOf
        {
            get { return m_isRealizationOf; }
            set { m_isRealizationOf = value; }
        }

        #endregion

        //========================================================================================
    }
}