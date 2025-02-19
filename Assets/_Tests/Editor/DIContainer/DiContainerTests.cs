using ADC._Tests.Editor.DIContainer.DummyCodes;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace ADC._Tests.Editor.DIContainer
{
// The DI container and supporting types from your code
// (Assume the DiContainer, InjectAttribute, DiException, MonoInjector, etc. are available in your project)

[TestFixture]
public class DiContainerTests
{
    private DiContainer _container;
    private Mock<IDummyService> _mockService;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IDummyService>();
        _container = new DiContainer();
    }

    [Test]
    public void RegisterSingleton_ByInstance_And_Resolve_ReturnsSameInstance()
    {
        // Arrange
        var instance = new DummyNoDependency();
        _container.RegisterSingleton(instance);

        // Act
        var resolved = _container.Resolve<DummyNoDependency>();

        // Assert
        Assert.AreSame(instance, resolved);
    }

    [Test]
    public void RegisterSingleton_ByMapping_And_Resolve_ReturnsInstanceOfImplementation()
    {
        // Arrange
        _container.RegisterSingleton<IDummyService, DummyService>();

        // Act
        var resolved = _container.Resolve<IDummyService>();

        // Assert
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOf<DummyService>(resolved);
        // Also check that the resolved dependency works
        Assert.AreEqual("Hello", resolved.GetData());
    }

    [Test]
    public void RegisterFactory_And_Resolve_ReturnsNewInstanceEachTime()
    {
        // Arrange
        _container.RegisterFactory(() => new DummyFactoryObject());

        // Act
        var first = _container.Resolve<DummyFactoryObject>();
        var second = _container.Resolve<DummyFactoryObject>();

        // Assert
        Assert.IsNotNull(first);
        Assert.IsNotNull(second);
        // Since factory creates a new instance each time, they should not be the same
        Assert.AreNotSame(first, second);
        Assert.AreNotEqual(first.Id, second.Id);
    }

    [Test]
    public void Resolve_NonRegisteredConcreteType_CreatesInstanceAutomatically()
    {
        // Arrange & Act
        var instance = _container.Resolve<DummyNoDependency>();

        // Assert
        Assert.IsNotNull(instance);
        Assert.AreEqual(42, instance.Value);
    }

    [Test]
    public void Resolve_NonRegisteredInterface_ThrowsDiException()
    {
        // Arrange & Act & Assert
        Assert.Throws<DiException>(() => _container.Resolve<IDummyService>());
    }

    [Test]
    public void InjectDependencies_SetsInjectedFields_OnTargetObject()
    {
        // Arrange
        _container.RegisterSingleton<IDummyService, DummyService>();
        var consumer = new DummyConsumer();

        // Pre-assert: field is null
        Assert.IsNull(consumer.Service);

        // Act
        _container.InjectDependencies(consumer);

        // Assert
        Assert.IsNotNull(consumer.Service);
        Assert.AreEqual("Hello", consumer.Service.GetData());
    }

    [Test]
    public void CreateInstance_AutomaticallyInjectsDependencies()
    {
        // Arrange
        _container.RegisterSingleton<IDummyService, DummyService>();

        // Act: call Resolve on a type that is not registered, but which has an [Inject] field.
        var consumer = _container.Resolve<DummyConsumer>();

        // Assert
        Assert.IsNotNull(consumer);
        Assert.IsNotNull(consumer.Service);
        Assert.AreEqual("Hello", consumer.Service.GetData());
    }

    [Test]
    public void PendingInjection_IsTriggered_WhenSingletonIsRegisteredLater()
    {
        // Arrange
        // Simulate a situation where an instance is created (via factory or new) that depends on IDummyService,
        // but no mapping exists yet. (In this DI container design, pending injections are used internally
        // when RegisterSingleton(T instance) is called, so we simulate that scenario.)
        var consumer = new DummyConsumer();

        // At this point, Service is not set because the container doesn't know about IDummyService.
        // Now, register the singleton instance.
        var service = new DummyService();
        _container.RegisterSingleton<IDummyService>(service);

        // Act: Now inject dependencies again.
        _container.InjectDependencies(consumer);

        // Assert
        Assert.IsNotNull(consumer.Service);
        Assert.AreSame(service, consumer.Service);
    }
}

#region MonoInjector Tests


public class MonoInjectorTests
{
    private GameObject _gameObject;
    private DiContainer _container;

    [SetUp]
    public void Setup()
    {
        // Create a temporary GameObject for testing.
        _gameObject = new GameObject("TestObject");

        // Use a fresh container for testing injection.
        _container = new DiContainer();
        // Register a dummy dependency.
        _container.RegisterSingleton(new DummyNoDependency { Value = 100 });

        // For testing, we bypass MonoInjector.Container and directly call InjectDependencies.
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(_gameObject);
    }
    //
    // [UnityTest]
    // public System.Collections.IEnumerator MonoBehaviourInjection_Works()
    // {
    //     // Arrange
    //     var dummyMB = _gameObject.AddComponent<DummyMonoBehaviour>();
    //
    //     // Act
    //     // Manually inject dependencies into all components on the GameObject.
    //     _container.InjectDependencies(dummyMB);
    //
    //     // Wait one frame (if needed)
    //     yield return null;
    //
    //     // Assert
    //     Assert.IsNotNull(dummyMB.injectedDependency);
    //     Assert.AreEqual(100, dummyMB.injectedDependency.Value);
    // }
}

#endregion
}