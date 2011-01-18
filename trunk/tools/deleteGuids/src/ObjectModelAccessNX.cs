/*! \file ObjectModelAccessNx.cs
 *  \brief Loads a NX part file.
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		06/2010
 */

using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.UF;


namespace deleteGuids
{
    public class ObjectModelAccessNX : ObjectModelAccessImpl
    {
        //****************************************************************************************

        #region Construction

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Loads a NX part file.
        /// </summary>
        /// <param name="partPath">The path to a NX part file (.prt)</param>
        //-------------------------------------------------------------------------//
        public ObjectModelAccessNX(string partPath)
        {
            m_Session = Session.GetSession();
            m_UFSession = UFSession.GetUFSession();

            m_PartPath = partPath;
            m_RootPart = m_Session.Parts.Open(PartPath, out m_PartLoadStatus);
        }

        #endregion

        //****************************************************************************************

        #region File processing

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Save any changes to a NX part file.
        /// </summary>
        //-------------------------------------------------------------------------//
        public override void Save()
        {
            m_PartSaveStatus = m_RootPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
            m_PartSaveStatus.Dispose();
        }

        #endregion

        //****************************************************************************************
    }
}