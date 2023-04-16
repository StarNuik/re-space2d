using SF = UnityEngine.SerializeField;

namespace PolygonArcana.Essentials
{
	public struct BoolTrigger
	{
		private bool __value;

		//> technically, as a read mutates data
		//> a method should be used to get the value,
		//> instead of a getter
		//> but oh well
		public bool Value
		{
			get => Get();
			set => Set(value);
		}

		public bool Get()
		{
			var old = __value;
			__value = false;
			return old;
		}

		public void Set(bool value)
		{
			__value = value;
		}

		public static implicit operator bool(BoolTrigger @this) => @this.Get();
	}
}
