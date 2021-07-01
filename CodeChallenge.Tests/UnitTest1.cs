using System;
using Xunit;

namespace CodeChallenge.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FindWayWhithCorrectString()
        {
            //Arrange
            string str = "5x5 (1, 3) (4, 4)";
            //Act
            SparseMatrix sparseMatrix =  SparseMatrix.Parse(str);
            string way = sparseMatrix.FindWay();
            //Assert
            Assert.Equal("ENNNDEEEND", way);
        }

        [Fact]
        public void CheckCorrectSizeAndCoord()
        {
            //Arrange
            string str = "5x5 (1, 3) (4,4)";
            //Act
            SparseMatrix sparseMatrix = SparseMatrix.Parse(str);
            //Assert
            Assert.Equal(5, sparseMatrix.Cols);
            Assert.Equal(5,sparseMatrix.Rows);
            Assert.Equal(1, sparseMatrix[1, 3]);//if coord exist her value in dictionary is equal 1          
            Assert.Equal(1, sparseMatrix[4, 4]);
        }

        [Fact]
        public void CheckCorrectExceptionWhithUncorrectString()
        {
            //Arrange
            string str = "5rx5 (1, 3) (4,4)";
            //Art
            var ex = Assert.Throws<ArgumentException>(() => SparseMatrix.Parse(str));
            //Assert
            Assert.Contains("Incorrect line entered", ex.Message);
        }

        [Fact]
        public void CheckCorrectExceptionWhithUncorrectIndex()
        {
            //Arrange
            string str = "5x5 (1, 3) (4,4)";
            //Art
            SparseMatrix sparseMatrix = SparseMatrix.Parse(str);
            //Assert            
            var ex = Assert.Throws<IndexOutOfRangeException>(() => sparseMatrix[12, 12]);
            Assert.Contains($"Index {12} must be from 0 to {sparseMatrix.Rows - 1}", ex.Message);
        }
    }
}
