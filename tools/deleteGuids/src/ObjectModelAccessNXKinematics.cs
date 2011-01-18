/*! \file ObjectModelAccessNxKinematics.cs
 *  \brief Loads a NX simulation file.
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
using NXOpen.Motion;
using NXOpen.UF;
using NXOpen.Utilities;


namespace deleteGuids
{
    public class ObjectModelAccessNXKinematics : ObjectModelAccessImpl
    {
        //****************************************************************************************

        #region Construction

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Access to NX simulation information of a given motion file
        /// </summary>
        /// <param name="MotionSimName">The name of the .sim file</param>
        /// <param name="OMA">A ObjectModelAccess_NX object required for general information access</param>
        //-------------------------------------------------------------------------//
        public ObjectModelAccessNXKinematics(string partPath, string motionSimName)
        {
            PartPath = partPath;
            m_MotionSimName = motionSimName;

            m_Session = Session.GetSession();
            m_UFSession = UFSession.GetUFSession();
            m_RootPart = m_Session.Parts.Open(PartPath, out m_PartLoadStatus);

            m_Session.MotionSimulation.LoadSimulation(m_RootPart, m_MotionSimName);
            m_Session.MotionSession.InitializeMechanisms();

            m_RootPart = m_Session.Parts.Work;
        }

        #endregion

        //****************************************************************************************

        #region File processing

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Save any changes to a NX simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public override void Save()
        {
            m_Session.MotionSimulation.SaveSimulation(m_RootPart, m_MotionSimName);
        }

        #endregion

        //****************************************************************************************
    }
}