//package com.wine.managedBean;
//
//import java.util.logging.Level;
//import java.util.logging.Logger;
//
//import javax.annotation.PostConstruct;
//import javax.ejb.EJB;
//import javax.faces.application.FacesMessage;
//import javax.faces.bean.ManagedBean;
//import javax.faces.bean.RequestScoped;
//import javax.faces.context.FacesContext;
//
//import com.shop.wineDAO.GlobalUserDAORemote;
//import com.shop.wineDTO.ChangePasswordDTO;
//import com.shop.wineDTO.GlobalUserDTO;
//import com.wine.exception.ChangePasswordException;
//
//@ManagedBean
//@RequestScoped
//public class ChangePasswordBean {
//
//	static final Logger LOGGER = Logger.getLogger(ChangePasswordBean.class.getName());
//
//	ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
//
//	@EJB
//	GlobalUserDAORemote userDAORemote;
//
//	GlobalUserDTO userDTO;
//
//	@PostConstruct
//	public void init() {
//		FacesContext facesContext = FacesContext.getCurrentInstance();
//		if (facesContext.getExternalContext().getFlash().get("user") != null) {
//			userDTO = (GlobalUserDTO) facesContext.getExternalContext().getFlash().get("user");
//		}
//	}
//
//	public String changePassword() {
//
//		LOGGER.log(Level.INFO, this.getClass().getName() + " Changing password: " + changePasswordDTO.toString());
//		FacesContext facesContext = FacesContext.getCurrentInstance();
//		try {
//			userDAORemote.updatePassword(changePasswordDTO);
//			facesContext.addMessage("changePassForm",
//					new FacesMessage(FacesMessage.SEVERITY_INFO, "Password changed successfully!", null));
//			return "success";
//		} catch (ChangePasswordException e) {
//			facesContext.addMessage("changePassForm",
//					new FacesMessage(FacesMessage.SEVERITY_ERROR, e.getMessage(), null));
//			return null;
//		}
//	}
//
//	public ChangePasswordDTO getChangePasswordDTO() {
//		return changePasswordDTO;
//	}
//
//	public void setChangePasswordDTO(ChangePasswordDTO changePasswordDTO) {
//		this.changePasswordDTO = changePasswordDTO;
//	}
//
//	public GlobalUserDAORemote getUserDAORemote() {
//		return userDAORemote;
//	}
//
//	public void setUserDAORemote(GlobalUserDAORemote userDAORemote) {
//		this.userDAORemote = userDAORemote;
//	}
//
//	public GlobalUserDTO getUserDTO() {
//		return userDTO;
//	}
//
//	public void setUserDTO(GlobalUserDTO userDTO) {
//		this.userDTO = userDTO;
//	}
//
//}