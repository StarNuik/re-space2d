using Zenject;

namespace PolygonArcana.Factories
{
	public class ClassFactory
	{
		private DiContainer container;

		public ClassFactory(DiContainer container)
		{
			this.container = container;
		}

		public T Create<T>()
			where T : class
		{
			var instance = container.Instantiate<T>();
			return Inject(instance);
		}

		public T Create<T, TArg>(TArg arg)
			where T : class
		{
			var instance = container.Instantiate<T>(new object[] { arg });
			return Inject(instance);
		}

		public T Create<T, TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
			where T : class
		{
			var instance = container.Instantiate<T>(new object[] { arg1, arg2 });
			return Inject(instance);
		}

		public T CreateDynamic<T>(params object[] args)
			where T : class
		{
			var instance = container.Instantiate<T>(args);
			return Inject(instance);
		}

		private T Inject<T>(T target)
			where T : class
		{
			container.QueueForInject(target);
			return target;
		}
	}
}