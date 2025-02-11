using Sisus.Init;
using ADC.API;

namespace ADC
{
	/// <summary>
	/// Initializer for the <see cref="ProtectiveInspiration"/> scriptable object.
	/// </summary>
	internal sealed class ProtectiveInspirationInitializer : ScriptableObjectInitializer<ProtectiveInspiration, IVFXPoolingManager>
	{
		#if UNITY_EDITOR
		/// <summary>
		/// This section can be used to customize how the Init arguments will be drawn in the Inspector.
		/// <para>
		/// The Init argument names shown in the Inspector will match the names of members defined inside this section.
		/// </para>
		/// <para>
		/// Any PropertyAttributes attached to these members will also affect the Init arguments in the Inspector.
		/// </para>
		/// </summary>
		private sealed class Init
		{
			public IVFXPoolingManager IVFXPoolingManager = default;
		}
		#endif
	}
}
