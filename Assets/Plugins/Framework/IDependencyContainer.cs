﻿using System;
using JetBrains.Annotations;

namespace Framework
{
	public interface IDependencyContainer
	{
		bool TryResolve([NotNull] Type type, out object dependency);
		bool CanBeResolvedSafe([NotNull] Type type);
	}
}