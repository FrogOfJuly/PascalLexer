using System;
using System.Linq;
using Antlr4.Runtime;
using NUnit.Framework;

namespace PascalLexer
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            const string input = "     ";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(6, tokenList.Count);
            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual(pascal.WS, tokenList[i].Type);
            }
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test2()
        {
            const string input = "%10100100 $AAaa57 &0126";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(6, tokenList.Count);
            Assert.AreEqual(pascal.UNUM, tokenList[0].Type);
            Assert.AreEqual(pascal.WS, tokenList[1].Type);
            Assert.AreEqual(pascal.UNUM, tokenList[2].Type);
            Assert.AreEqual(pascal.WS, tokenList[3].Type);
            Assert.AreEqual(pascal.UNUM, tokenList[4].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test3()
        {
            const string input = "-%10100100 +$AAaa57 -&0126";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(6, tokenList.Count);
            Assert.AreEqual(pascal.SNUM, tokenList[0].Type);
            Assert.AreEqual(pascal.WS, tokenList[1].Type);
            Assert.AreEqual(pascal.SNUM, tokenList[2].Type);
            Assert.AreEqual(pascal.WS, tokenList[3].Type);
            Assert.AreEqual(pascal.SNUM, tokenList[4].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test4()
        {
            const string input = "//-%10100100 +$AAaa57 -&0126";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(2, tokenList.Count);
            Assert.AreEqual(pascal.SINGLE_LINE_COMMENT, tokenList[0].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test5()
        {
            const string input = "//-%10100100 +$AAaa57 -&0126 \n// adskfajslkd";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(4, tokenList.Count);
            Assert.AreEqual(pascal.SINGLE_LINE_COMMENT, tokenList[0].Type);
            Assert.AreEqual(pascal.WS, tokenList[1].Type);
            Assert.AreEqual(pascal.SINGLE_LINE_COMMENT, tokenList[2].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test6()
        {
            const string input = "(*-%10100100 +$AAaa57 -&0126 \n// adskfajslkd*)";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(2, tokenList.Count);
            Assert.AreEqual(pascal.MLINE_COMMENT, tokenList[0].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test7()
        {
            const string input = "id1 id2 id3";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(6, tokenList.Count);
            Assert.AreEqual(pascal.ID, tokenList[0].Type);
            Assert.AreEqual(pascal.WS,tokenList[1].Type);
            Assert.AreEqual(pascal.ID, tokenList[2].Type);
            Assert.AreEqual(pascal.WS,tokenList[3].Type);
            Assert.AreEqual(pascal.ID, tokenList[4].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
        
        [Test]
        public void Test8()
        {
            const string input = "\'id1 id2 id3\'";
            var inputStream = new AntlrInputStream(input);
            var lexer = new pascal(inputStream);
            var tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            var tokenList = tokens.GetTokens();
            Assert.AreEqual(2, tokenList.Count);
            Assert.AreEqual(pascal.CHARCACTER_STRING, tokenList[0].Type);
            Assert.AreEqual(pascal.Eof,tokenList.Last().Type);
            Assert.Pass();
        }
    }
}