/*! \file NXObjectExtensions.cs
 *  \brief Extension methods for NXOpen.NXObject
 * 
 *  This class contains extension methods for NXOpen.NXObject methods.
 *  
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		06/2010
 */

using System;
using NXOpen;

namespace deleteGuids
{
    public static class NXObjectExtensions
    {
        //****************************************************************************************

        #region NXObject Extensions

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the GUID of a given NXObject object (extends NXOpen.NXObject).
        /// </summary>
        /// <param name="obj">The NXObject object</param>
        /// <returns>A string attribute, which contains a GUID</returns>
        //-------------------------------------------------------------------------//
        public static string GetGuid(this NXObject obj)
        {                                    
            try
            {
                return obj.GetStringAttribute("ID");
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion

        //****************************************************************************************
    }
}
