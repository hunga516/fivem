using System.Security;
using CitizenFX.Core.Native;

namespace CitizenFX.Core
{
	public class StateBag
	{
		private CString m_bagName;

		static StateBag Global => new StateBag("global");

		internal StateBag(string bagName)
		{
			m_bagName = (CString)bagName;
		}

		[SecuritySafeCritical]
		public unsafe void Set(string key, object data, bool replicate)
			=> CoreNatives.SetStateBagValue(m_bagName, key, InPacket.Serialize(data), replicate);

		public dynamic Get(string key)
			=> CoreNatives.GetStateBagValue(m_bagName, key);

		public dynamic this[string key]
		{
			get => Get(key);
			set => Set(key, value, Resource.IsServer);
		}
	}
}
