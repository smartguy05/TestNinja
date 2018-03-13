using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperHelperTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IHouseKeeperFunctions> _houseKeeperFunctions;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private HousekeeperHelper _service;
        private readonly DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;
        private string _statementFileName;

        [SetUp]
        public void Setup()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "filename";

            _houseKeeperFunctions = new Mock<IHouseKeeperFunctions>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _service = new HousekeeperHelper(_unitOfWork.Object, _houseKeeperFunctions.Object, _xtraMessageBox.Object);

            _houseKeeperFunctions
                .Setup(hk =>
                    hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)))
                .Returns(() => _statementFileName);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _houseKeeperFunctions.Verify(hk => hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            _houseKeeperFunctions.Verify(hk => 
                hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)
                , Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhitespace_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            _houseKeeperFunctions.Verify(hk =>
                    hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)
                , Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmtpy_ShouldNotGenerateStatement()
        {
            _housekeeper.Email = string.Empty;

            _service.SendStatementEmails(_statementDate);

            _houseKeeperFunctions.Verify(hk =>
                    hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)
                , Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_ShouldEmailStatement()
        {
            
            _houseKeeperFunctions
                .Setup(hk =>
                hk.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)))
                .Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);
            
            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailStatement()
        {
            _statementFileName = null;
            
            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }


        [Test]
        public void SendStatementEmails_StatementFileNameIsEmpyString_ShouldNotEmailStatement()
        {
            _statementFileName = string.Empty;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }


        [Test]
        public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailStatement()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _houseKeeperFunctions.Setup(hk =>
                hk.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplayed();
        }

        private void VerifyMessageBoxDisplayed()
        {
            _xtraMessageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _houseKeeperFunctions.Verify(es =>
                    es.EmailFile(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                Times.Never);
        }


        private void VerifyEmailSent()
        {
            _houseKeeperFunctions.Verify(es =>
                es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()));
        }
}
}
