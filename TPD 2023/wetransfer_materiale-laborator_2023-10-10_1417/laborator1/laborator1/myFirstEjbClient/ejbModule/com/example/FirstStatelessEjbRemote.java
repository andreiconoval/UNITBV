package com.example;

import javax.ejb.Remote;

@Remote
public interface FirstStatelessEjbRemote {

	void insert(String name);
}
