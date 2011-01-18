/*! \file IInformationModelAccess.cs
 *  \brief An interface for the InformationModelAccess class
 *
 *  This interface contains all members, which should be implemented in the InformationModelAccess class.
 *  
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		08/2010
 */

using System.Collections.Generic;

namespace Schnittstelle_NX
{
    interface IInformationModelAccess
    {
        //========================================================================================

        #region Repository operations

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Export all data from ontology into a owl file (RDF/XML)
        /// </summary>
        //-------------------------------------------------------------------------//
        void Export();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Export all data from ontology into a owl file (RDF/XML)
        /// </summary>
        //-------------------------------------------------------------------------//
        void Export(string filePath);

        void Connect();

        void Connect(string customOwlDir);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Get the unit name, which is specified in the information model.
        /// </summary>
        /// <param name="unit">The NX unit name</param>
        /// <returns>The unit name of the information model</returns>
        //-------------------------------------------------------------------------//
        string GetUnitName(string unit);

        #endregion

        //========================================================================================

        #region Add* methods

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all component identifiers of an assembly to the ontology.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddComponents();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds information about child components.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddChildComponents();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds information about parent components.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddParentComponents();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Since every component is represented by an ID, this method adds the
        /// display name of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddComponentNames();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all component positions.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddComponentPositions();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds 'Bounding Box' coordinates of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddRelationalBoundingBoxes();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds hole features of every component.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddHoleFeatures();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds the location of hole.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddHolePositions();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds the direction of every hole.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddHoleDirections();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all hole diameters.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddHoleExpressions();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Add all joints (identifier). Required for kinematics extraction.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddJoints();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Add all links (identifier). Required for kinematics extraction.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddLinks();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds component mass
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddMassOfComponents();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds mass moment of inertia (WCS)
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddMomentsOfInertiaInMassCenter();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Adds all mechanical connections between components realized by a 
        /// mechanical connector.
        /// </summary>
        //-------------------------------------------------------------------------//
        void AddMechanicalConnections();

        #endregion

        //========================================================================================

        #region Create information model

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        void BeginExtraction();

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        void BeginExtraction(List<string> methodList);

        //-------------------------------------------------------------------------//
        /// <summary>
        /// Executes all Add* methods successively.
        /// </summary>
        //-------------------------------------------------------------------------//
        void BeginExtraction(string method);

        #endregion

        //========================================================================================
    }
}
