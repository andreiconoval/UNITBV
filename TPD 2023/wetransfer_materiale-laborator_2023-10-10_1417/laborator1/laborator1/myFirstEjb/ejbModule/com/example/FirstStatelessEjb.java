package com.example;

import javax.ejb.LocalBean;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import com.example.model.User;

/**
 * Session Bean implementation class FirstStatelessEjb
 */
@Stateless
@LocalBean
public class FirstStatelessEjb implements FirstStatelessEjbRemote {

	@PersistenceContext
	private EntityManager entityManager;

	/**
	 * Default constructor.
	 */
	public FirstStatelessEjb() {
		// TODO Auto-generated constructor stub
	}

	@Override
	public void insert(String name) {
		entityManager.persist(new User(name));
	}
}
