using Sisus.Init;
using ADC.API;
using RTSEngine.Game;

namespace ADC.UnitCreation
{
	/// <summary>
	/// Initializer for the <see cref="CellFillerComponent"/> component.
	/// </summary>
	internal sealed class CellFillerComponentInitializer : Initializer<CellFillerComponent, IEconomySystem, IDeactivablesManager, IGameManager>
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
			public IEconomySystem EconomySystem = default;
			public IDeactivablesManager DeactivablesManager = default;
			public IGameManager GameManager = default;
		}
		#endif
	}
}
