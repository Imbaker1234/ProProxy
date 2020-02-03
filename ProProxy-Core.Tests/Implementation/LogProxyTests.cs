using ProProxy;
using System;
using System.Dynamic;
using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace ProProxy_Core.Tests.Implementation
{
    public class LogProxyTests
    {
        [Fact]
        public void As_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new Fixture().Customize(new AutoMoqCustomization());
            var instance = mocker.Create<IDummy>();
            Action preAction = null;
            Action postAction = null;
            Action responseOnFailure = null;

            // Act
            var newProxy = LogProxy<IDummy>(null, null, null, instance)
                .As<IDummy>();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void As_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var mocker = new AutoMoqer();
            var logProxy = mocker.Create<LogProxy>();
            LogShell ls = null;
            T instance = null;

            // Act
            var result = logProxy.As(
                ls,
                instance);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void TryInvokeMember_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new AutoMoqer();
            var logProxy = mocker.Create<LogProxy>();
            InvokeMemberBinder binder = null;
            object[] args = null;
            object result = null;

            // Act
            result = logProxy.TryInvokeMember(
                binder,
                args,
                out result);

            // Assert
            Assert.True(false);
        }
    }
}
