package com.example.dao;

import javax.ejb.Remote;

import com.example.dto.ChangePasswordDTO;
import com.example.dto.LoginDTO;
import com.example.dto.UserDTO;
import com.example.exception.ChangePasswordException;
import com.example.exception.LoginException;

@Remote
public interface UserDAORemote extends GenericDAO<UserDTO> {

	UserDTO loginUser(LoginDTO loginDTO) throws LoginException;

	Boolean updatePassword(ChangePasswordDTO changePasswordDTO) throws ChangePasswordException;
}
