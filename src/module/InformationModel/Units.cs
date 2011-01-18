/*! \file Units.cs
 *  \brief Access to unit names
 *  
 *  This class contains all units names as defined in our information model.
 * 
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		08/2010
 */

using Siemens.HCSS.Configuration;

namespace Schnittstelle_NX.InformationModel
{
    public class Units
    {
        //========================================================================================

        #region Fields

        // Individuals
        private string
            m_BaseUnit_Degree,
            m_BaseUnit_Kilogram,
            m_UnitDerivedByScaling_MilliMeter,
            m_DerivedUnit_ComplexUnit_UnitDerivedByRaisingToPower_KilogramMilliMeterSquare;

        #endregion

        //========================================================================================

        #region Properties

        // Degree

        [Configuration("BaseUnit\\degree", "degree")]
        public string Degree
        {
            get { return m_BaseUnit_Degree; }
            set { m_BaseUnit_Degree = value; }
        }

        // Kilogram
        [Configuration("BaseUnit\\kilogram", "kilogram")]
        public string Kilogram
        {
            get { return m_BaseUnit_Kilogram; }
            set { m_BaseUnit_Kilogram = value; }
        }

        // Millimeter
        [Configuration("UnitDerivedByScaling\\milli_meter", "milli_meter")]
        public string MilliMeter
        {
            get { return m_UnitDerivedByScaling_MilliMeter; }
            set { m_UnitDerivedByScaling_MilliMeter = value; }
        }

        // KilogramMilliMeterSquare
        [Configuration("DerivedUnit\\ComplexUnit\\UnitDerivedByRaisingToPower\\kilogram_milli_meterSquare", "kilogram_milli_meterSquare")]
        public string KilogramMilliMeterSquare
        {
            get { return m_DerivedUnit_ComplexUnit_UnitDerivedByRaisingToPower_KilogramMilliMeterSquare; }
            set { m_DerivedUnit_ComplexUnit_UnitDerivedByRaisingToPower_KilogramMilliMeterSquare = value; }
        }

        #endregion

        //========================================================================================
    }
}