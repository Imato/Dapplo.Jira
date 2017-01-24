﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Worklog information
	/// </summary>
	[DataContract]
	public class Worklog : BaseProperties<string>
	{
		public Worklog()
		{
			
		}

		public Worklog(TimeSpan timeSpent)
		{
			TimeSpentSeconds = (int)timeSpent.TotalSeconds;
		}

		/// <summary>
		///     Author of this worklog
		/// </summary>
		[DataMember(Name = "author", EmitDefaultValue = false)]
		public User Author { get; set; }

		/// <summary>
		///     Comment for this worklog
		/// </summary>
		[DataMember(Name = "comment", EmitDefaultValue = false)]
		public string Comment { get; set; }

		/// <summary>
		///     When was the worklog created
		/// </summary>
		[DataMember(Name = "created", EmitDefaultValue = false)]
		public DateTimeOffset Created { get; set; }

		/// <summary>
		///     When was the worklog started
		/// </summary>
		[DataMember(Name = "started", EmitDefaultValue = false)]
		public DateTimeOffset Started { get; set; }

		/// <summary>
		///     Time spent in this worklog, this is a number and qualifier (h = hour, d = day etc)
		/// </summary>
		[DataMember(Name = "timeSpent", EmitDefaultValue = false)]
		public string TimeSpent { get; set; }

		/// <summary>
		///     Time spent in this worklog, in seconds
		/// </summary>
		[DataMember(Name = "timeSpentSeconds", EmitDefaultValue = false)]
		public long TimeSpentSeconds { get; set; }

		/// <summary>
		///     Who updated this worklog
		/// </summary>
		[DataMember(Name = "updateAuthor", EmitDefaultValue = false)]
		public User UpdateAuthor { get; set; }

		/// <summary>
		///     When was the worklog updated
		/// </summary>
		[DataMember(Name = "updated", EmitDefaultValue = false)]
		public DateTimeOffset Updated { get; set; }

		/// <summary>
		///     Visibility
		/// </summary>
		[DataMember(Name = "visibility", EmitDefaultValue = false)]
		public Visibility Visibility { get; set; }
	}
}