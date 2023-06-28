using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommunicationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterTest()
        {
            //arrage
            string l = "admin";
            string p = "123";
            string rp = "123";
            string expected = "Успешно!";
            //act
            Communication.RegistrWindow registr = new Communication.RegistrWindow();
            string actual = registr.reg("", l, p, rp);
            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LoginTest()
        {
            //arrage
            string l = "d";
            string p = "d";
            string expected = "Успешно!";
            //act
            Communication.MainWindow login = new Communication.MainWindow();
            string actual = login.testlog("", l, p);
            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AnswerButtonTest()
        {
            //arrage
            string sel = "13. dwadwdaefw ";
            int n = 0;
            string expected1 = "Успешно!";
            //act
            Communication.AdminCommunicationWindow adminCom = new Communication.AdminCommunicationWindow();
            string actual1 = adminCom.answertest("", n,sel);
            //assert
            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void AppAnsTest()
        {
            //arrage
            string ans = "Как установить процессор?";
            string expected1 = "Успешно!";
            //act
            Communication.AddQuestion addQuestion = new Communication.AddQuestion();
            string actual1 = addQuestion.AppAns("", ans);
            //assert
            Assert.AreEqual(expected1, actual1);
        }
        [TestMethod]
        public void DeleteProfileTest()
        {
            //arrage
            string mresult = "Yes";
            string prof = "admin";
            string expected1 = "Успешно!";
            //act
            Communication.ProfileWindow profileCom = new Communication.ProfileWindow();
            string actual1 = profileCom.Deletetest("",mresult,prof);
            //assert
            Assert.AreEqual(expected1, actual1);
        }
    }
}
