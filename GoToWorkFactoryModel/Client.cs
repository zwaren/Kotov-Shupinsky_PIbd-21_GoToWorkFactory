﻿using System.ComponentModel.DataAnnotations;

namespace GoToWorkFactoryModel
{
	public class Client
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
