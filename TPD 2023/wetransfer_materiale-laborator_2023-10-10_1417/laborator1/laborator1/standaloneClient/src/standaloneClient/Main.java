package standaloneClient;

import javax.naming.InitialContext;
import javax.naming.NamingException;

import com.example.FirstStatelessEjbRemote;

public class Main {

	public static void main(String[] args) throws NamingException {

		InitialContext context = new InitialContext();
		FirstStatelessEjbRemote firstEjb = (FirstStatelessEjbRemote) context
				.lookup("java:global/myFirstEar/myFirstEjb/FirstStatelessEjb!com.example.FirstStatelessEjbRemote");
		firstEjb.insert("Ion Chirobocia");
	}
}
