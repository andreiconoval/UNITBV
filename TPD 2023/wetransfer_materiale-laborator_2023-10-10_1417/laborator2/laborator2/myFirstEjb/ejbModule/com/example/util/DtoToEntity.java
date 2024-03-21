package com.example.util;

import com.example.dto.UserDTO;

import model.User;

public class DtoToEntity {

	// all classes doesn't take primary key in account

	public User convertUser(UserDTO userDTO) {
		User user = new User(userDTO.getUsername(), userDTO.getPassword());

		return user;
	}

}
