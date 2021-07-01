using System;

namespace CodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            SparseMatrix sparseMatrix = SparseMatrix.Parse(Console.ReadLine());
            Console.WriteLine(sparseMatrix.FindWay());
        }
    }
}
