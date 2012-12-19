//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents {

    public class Word {
        int occurrence = 0;
        int _occurenceWeight = 0;
        string word;

        public Word(string word, WordLocation location) {
            this.word = word;
            IncrementOccurence(location);
        }

        public void IncrementOccurence(WordLocation location) {

            //ScottW:
            //This may need to be Application specific since only blog has Excerpt. 
            //It should not break the search logic, but just a little messy to drop it
            //in here.
            switch(location)
            {
                case WordLocation.Section:
                        _occurenceWeight += 5;
                    break;
                case WordLocation.Excerpt:
                    _occurenceWeight += 5;
                    break;
                case WordLocation.Subject:
                    _occurenceWeight += 10;
                    break;
                default:
                    _occurenceWeight++;
                    break;

            }
        }

        public string Name {
            get {
                return word;
            }
        }

        public int OccurenceWeight {
            get {
                return _occurenceWeight;
            }
        }

        public int Occurence {
            get {
                return occurrence;
            }
        }

        private double _weight;
        /// <summary>
        /// Property Weight (double)
        /// </summary>
        public double Weight
        {
            get {  return this._weight; }
            set {  this._weight = value; }
        }

    }


}
