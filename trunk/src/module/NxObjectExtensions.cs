/*! \file NXObjectExtensions.cs
 *  \brief Extension methods for NXOpen.NXObject
 * 
 *  This class contains extension methods for NXOpen.NXObject objects.
 *  
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		05/2010
 */

using System;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Motion;
using NXOpen.Features;

namespace Schnittstelle_NX
{
    public static class NxObjectExtensions
    {
        //========================================================================================

        #region NXObject Extensions

        private static Config s_Config = new Config();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the GUID of a given NXObject object (extends NXOpen.NXObject).
        /// </summary>
        /// <param name="obj">The NXObject object</param>
        /// <returns>A string attribute, which contains a GUID</returns>
        //-------------------------------------------------------------------------//
        public static string GetGuid(this NXObject obj)
        {
            string className;

            if (obj is Component)
                className = s_Config.Class.PhysicalComponent;
            else if (obj is Link)
                className = s_Config.Class.Link;
            else if (obj is Joint)
                className = s_Config.Class.Joint;
            else if (obj is BodyFeature)
                className = s_Config.Class.HoleFeature;
            else className = "OBJECT";


            try
            {
                if (!obj.GetStringAttribute("ID").Contains(className)) throw new NotSupportedException();
                return obj.GetStringAttribute("ID");
            }
            catch (Exception)
            {
                string id = className + "-" + Guid.NewGuid().ToString();
                obj.SetAttribute("ID", id);
                return id;
            }
        }

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Sets an ID attribute for a NXObject.
        /// </summary>
        /// <param name="obj">The NXObject</param>
        /// <param name="guid_string">The value for the ID attribute</param>
        /// <returns>true, if guid was set</returns>
        //-------------------------------------------------------------------------//
        public static bool SetGuid(this NXObject obj, string guid_string)
        {
            obj.SetAttribute("ID", guid_string);
            return true;
        }

        #endregion

        //========================================================================================
    }
}
