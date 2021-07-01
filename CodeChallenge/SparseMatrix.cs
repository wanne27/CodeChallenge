using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge
{
    public class SparseMatrix
    {
        private readonly Dictionary<(int, int), int> elements = new Dictionary<(int, int), int>();
        
        public int Rows { get; }
        public int Cols { get; }

        public int this[int i, int j]
        {
            get
            {
                CheckIndexes(i, j);
                return elements.GetValueOrDefault((i, j));
            }
            set
            {
                CheckIndexes(i, j);
                var key = (i, j);
                if (value == 0)
                {
                    if (elements.ContainsKey(key))
                    {
                        elements.Remove(key);
                    }
                }
                else
                {
                    elements[key] = value;
                }
            }
        }

        public SparseMatrix(int rows, int cols)
        {
            CheckArgument(rows, nameof(rows));
            CheckArgument(cols, nameof(cols));

            Rows = rows;
            Cols = cols; 

            static void CheckArgument(int value, string name)
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Must be greater than 0", name);
                }
            }
        }

        public static SparseMatrix Parse(string s)
        {
            char[] separators = new char[] { ' ', 'x', '(', ',', ')' };
            string[] subs = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            SparseMatrix matrix;

            try 
            {
                 matrix = new SparseMatrix(Int32.Parse(subs[0]), Int32.Parse(subs[1]));
            }
            catch
            {
                throw new ArgumentException("Incorrect line entered");
            }           

            int count = 2;
            for (int i = 2; i < subs.Length; i = i + 2) 
            {
                if( matrix[Int32.Parse(subs[i]), Int32.Parse(subs[i + 1])] == default)
                {
                    matrix[Int32.Parse(subs[i]), Int32.Parse(subs[i + 1])] = 1;
                }
                else
                {
                    matrix[Int32.Parse(subs[i]), Int32.Parse(subs[i + 1])] = count;
                    count++;
                }
            }
              
            return matrix;
        }

        public string FindWay()
        {
            int helpIndexi = 0;
            int helpIndexj = 0;
            string way = null;

            for (int i = 0; i < Rows; i++) 
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (this[i, j] != default)
                    {
                        helpIndexi = i - helpIndexi;
                        helpIndexj = j - helpIndexj;
                       
                        if (helpIndexi >= 0 && helpIndexj >= 0)
                        {
                            way += new string('E', helpIndexi);
                            way += new string('N', helpIndexj);
                            way += new string('D', this[i, j]);
                        }
                        else if (helpIndexi <= 0 && helpIndexj <= 0)
                        {
                            way += new string('W', helpIndexi * -1);
                            way += new string('S', helpIndexj * -1);
                            way += new string('D', this[i, j]);
                        }
                        else if (helpIndexi >= 0 && helpIndexj <= 0)
                        {
                            way += new string('E', helpIndexi);
                            way += new string('S', helpIndexj * -1);
                            way += new string('D', this[i, j]);
                        }
                        else if (helpIndexi <= 0 && helpIndexj >= 0)
                        {
                            way += new string('W', helpIndexi * -1);
                            way += new string('N', helpIndexj);
                            way += new string('D', this[i, j]);
                        }

                        helpIndexi = i;
                        helpIndexj = j;
                    }
                }
            }

            return way;
        }

        private void CheckIndexes(int i, int j)
        {
            if (i < 0 || i >= Rows) 
            { 
                throw new IndexOutOfRangeException($"Index {i} must be from 0 to {Rows - 1}");
            }

            if (j < 0 || j >= Cols)
            {
                throw new IndexOutOfRangeException($"Index {j} must be from 0 to {Cols - 1}");
            }
        }
    }
}
