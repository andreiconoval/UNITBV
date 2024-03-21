﻿using DataMapper.SqlServerDAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public static class DAOFactoryMethod
    {
        private static readonly IDAOFactory _currentDAOFactory;

        /// <summary>Initializes the <see cref="DAOFactoryMethod" /> class.</summary>
        static DAOFactoryMethod()
        {
            string currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (String.IsNullOrWhiteSpace(currentDataProvider))
            {
                _currentDAOFactory = null;
            }
            else
            {
                switch(currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        _currentDAOFactory = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        _currentDAOFactory = null;//de fapt ar trebui un new OracleDaoFactory, dar care nu e inca scris
                        return;
                    default:
                        _currentDAOFactory = new SQLServerDAOFactory();
                        break;
                }
            }
        }
        /// <summary>Gets the current DAO factory.</summary>
        /// <value>The current DAO factory.</value>
        public static IDAOFactory CurrentDAOFactory
        {
            get
            {
                return _currentDAOFactory;
            }
        }
    }
}
