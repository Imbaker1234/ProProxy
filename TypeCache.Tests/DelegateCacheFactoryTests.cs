using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using ProProxy_Core.Tests;
using Xunit;

namespace TypeCache.Tests
{
    public class DelegateCacheFactoryTests
    {
        [Fact]
        public void GenerateCache_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mocker = new Fixture().Customize(new AutoMoqCustomization());
            var instance = new Dummy();
            var cache = DelegateCacheFactory.Create(new Dummy());


            // Act
            var words = mocker.Create<string>();

            var a = mocker.Create<int>();
            var b = mocker.Create<int>();
            var c = mocker.Create<int>();


            // Assert
            instance.Listen(words).Should()
                .Be((string) cache["Listen"].DynamicInvoke(words));

            instance.Calculate(a, b, c).Should()
                .Be((int) cache["Calculate"].DynamicInvoke(a, b, c));
        }

        [Fact]
        public void Multiple_Caches_Will_CoExist()
        {
            var dummy1 = new Dummy {Age = 21};
            var dummy2 = new Dummy {Age = 32};

            var cache1 = DelegateCacheFactory.Create(dummy1);
            var cache2 = DelegateCacheFactory.Create(dummy2);

            cache2["set_Age"].Invoke(42);

            dummy1.Age.Should().Be(21);
            dummy2.Age.Should().Be(42);
        }
    }
}
