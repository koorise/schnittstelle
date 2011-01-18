/** \file MAIN.cs
 *  \brief The main entry point of this application.
 *  
 *  This class contains the 'Main' method, which parses command line arguments and starts the 
 *  extraction of NX information.
 * 
 * 
 *  \author     Denis Özdemir
 *  \author     Ilker Dogan
 *  \author		Werkzeugmaschinenlabor WZL der RWTH Aachen
 *  \date		04/2010
 */

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace deleteGuids
{
    class MAIN
    {
        //****************************************************************************************

        #region Main Entry Point

        //-------------------------------------------------------------------------//
        /// <summary>
        /// This is the main entry point of this application.
        /// </summary>
        //-------------------------------------------------------------------------//

        [STAThread]
        static void Main(string[] args)
        {
            ObjectModelAccessImpl objectModel;

            String partFilePath = null;
            String motionSimName = null;


            // Parse command line arguments
            switch (args.Length)
            {
                case 1:
                    partFilePath = args[0];
                    break;
                case 2:
                    partFilePath = args[0];
                    motionSimName = args[1];
                    break;
                default:
                    Console.WriteLine("Invalid arguments!");
                    return;
            }

            DateTime startTime = DateTime.Now;

            Console.WriteLine("\nInitializing NX...");

            // Initializing NX
            if (String.IsNullOrEmpty(motionSimName)) objectModel = new ObjectModelAccessNX(partFilePath);
            else objectModel = new ObjectModelAccessNXKinematics(partFilePath, motionSimName);


            Console.WriteLine("\nDeleting GUIDs...");

            int counter = 0;
            var componentIds = objectModel.GetAllComponents();

            foreach (string componentId in componentIds)
            {
                try
                {
                    objectModel.DeleteStringAttribute(componentId, "ID");
                }
                catch (Exception)
                {
                    counter++;
                }
            }

            DateTime endTime = DateTime.Now;
            TimeSpan timeUsed = endTime - startTime;

            Console.WriteLine("\nFinished!");
            Console.WriteLine("Time used: " + (int)timeUsed.TotalSeconds + " seconds\n");
            Console.WriteLine("\nSaving changes to \"" + Path.GetFileName(partFilePath) + "\"...");

            while (true)
            {
                Console.Write("Proceed? [Y/N]: ");

                // Awaiting user input
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        // Save NX part/simulation file
                        Console.WriteLine();
                        objectModel.Save();
                        break;
                    case ConsoleKey.N:
                        // Don't save!
                        Console.WriteLine("\nUser abort!");
                        break;
                    default:
                        Console.WriteLine();
                        continue;
                }
                break;
            }
        }

        #endregion

        //****************************************************************************************
    }
}