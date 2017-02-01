#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Information on the custom field id for the estimation information
	/// </summary>
	[DataContract]
	public class EstimationFieldInfo
	{
		/// <summary>
		///     Field name of the estimation custom field
		/// </summary>
		[DataMember(Name = "fieldId", EmitDefaultValue = false)]
		public string FieldId { get; set; }

		/// <summary>
		///     Display name for the estimation
		/// </summary>
		[DataMember(Name = "displayName", EmitDefaultValue = false)]
		public string DisplayName { get; set; }
	}
}