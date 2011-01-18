/*! \file IObjectModelAccess.cs
 *  \brief An interface for the ObjectModelAccess class
 *
 *  This interface contains all members, which should be implemented in the ObjectModelAccess class.
 *  
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		08/2010
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schnittstelle_NX
{
    interface IObjectModelAccess
    {
        //========================================================================================

        #region File Processing

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Open a NX part file.
        /// </summary>
        //-------------------------------------------------------------------------//
        void Open(string partPath);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Load a NX simulation.
        /// </summary>
        //-------------------------------------------------------------------------//
        void LoadSimulation(string motionSimName);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Reinitializes all fields and loads a given part file.
        /// </summary>
        //-------------------------------------------------------------------------//
        void Reload(string partFile, bool fSave);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Reinitializes all fields and loads a given part and simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        void Reload(string partFile, string simName, bool fSave);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Save any changes to a NX part and/or simulation file.
        /// </summary>
        //-------------------------------------------------------------------------//
        void Save();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Close all parts (even if modified).
        /// </summary>
        //-------------------------------------------------------------------------//
        void CloseAll();

        #endregion

        //========================================================================================

        #region Extraction methods

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the root component of the current assembly.
        /// </summary>
        /// <returns>The GUID of the root component</returns>
        //-------------------------------------------------------------------------//
        string GetRootComponent();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components.
        /// </summary>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetAllComponents();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components with given root component.
        /// </summary>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetAllComponents(string rootComponentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all child components of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetAllChildComponents(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the first level child components of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetChildComponents(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the parent component of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>One component identifier</returns>
        //-------------------------------------------------------------------------//
        string GetParentComponent(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the name of a component
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>The name of a component</returns>
        //-------------------------------------------------------------------------//
        string GetName(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the position of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetPosition(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the bounding box of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of coordinates (X_min, Y_min, Z_min, X_max, Y_max, Z_max)</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetBoundingBox(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all hole features.
        /// </summary>
        /// <returns>A list of hole feature identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetHoleFeatures();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the hole feature of a given component.
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of hole feature identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetHoleFeatures(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the position of a hole feature.
        /// </summary>
        /// <param name="holeId">The hole feature identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetHolePosition(string holeId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the direction of a hole feature.
        /// </summary>
        /// <param name="holeId">The hole feature identifier</param>
        /// <returns>A list of coordinates (X,Y,Z)</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetHoleDirection(string holeId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a list of hole expressions of a given hole, e.g. diameter, depth.
        /// 
        /// Each element contains 3 attributes:
        /// 
        /// [0] = Description
        /// [1] = Value
        /// [2] = Unit
        /// </summary>
        /// <param name="holeId">The hole identifier</param>
        /// <returns>A list of string arrays, which contain hole expressions</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string[]> GetHoleExpressions(string holeId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the mass of a given component. The mass might be incorrect 
        /// (incomplete), if a component has child components.
        /// 
        /// [0] = the component mass
        /// [1] = boolean; true, if incomplete
        /// [2] = the unit
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>[0] the mass; [1] boolean; true, if mass is incomplete; [2] the unit</returns>
        //-------------------------------------------------------------------------//
        List<string> GetMassOfComponent(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the moments of inertia (centroidal) of a given component 
        /// based on the part coordinate system.
        /// 
        /// key = moments of inertia
        /// value = unit
        /// </summary>
        /// <param name="componentId">The component identifier</param>
        /// <returns>A list of inertia values as string</returns>
        //-------------------------------------------------------------------------//
        KeyValuePair<string[], string> GetMomentsOfInertia(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the length unit of the base part.
        /// </summary>
        /// <returns>The length unit of the base part.</returns>
        //-------------------------------------------------------------------------//
        string GetPartUnit();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns a list of all Connector components
        /// </summary>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetAllComponentConnectors();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Returns all components that are connected to an assembly component (screw etc.)
        /// </summary>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetComponentsConnectedWithConnector(string componentId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all links.
        /// </summary>
        /// <returns>A list of link identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetLinks();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all joints.
        /// </summary>
        /// <returns>A list of joint identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetJoints();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all components of a given link.
        /// </summary>
        /// <param name="linkId">The link identifier</param>
        /// <returns>A list of component identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetComponentsOfLink(string linkId);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get all links of a given joint.
        /// </summary>
        /// <param name="jointId">The joint identifier</param>
        /// <returns>A list of link identifiers</returns>
        //-------------------------------------------------------------------------//
        IEnumerable<string> GetLinksOfJoint(string jointId);

        #endregion

        //========================================================================================
    }
}
