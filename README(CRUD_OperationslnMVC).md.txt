Pattern 1.
ASP.NET MVC design pattern - EntityFramework

Call Controller :
  
        public ActionResult Index()
        {
        EmployeeDBEntities dbContext = new EmployeeDBEntities();
        List < Department > listDepartments = dbContext.Departments.ToList();
        listEmployee = dbContext.Employees.ToList();
        return View(listDepartments);
        }
     
ASP.NET MVC design pattern - EntityFramework, Business object using stored procedure

Call Controller :
        // named GetAllMembers()=> Stored Procedure call
         public ActionResult Index()
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                List < Member > members = memberBusinessLayer.GetAllMembers();
                return View(members);
            }
    
Business logic :
   class MemberBusinessLayer  
    {
        public List < Member > GetAllMembers()
        {
            //Reads the connection string from web.config file. The connection string name is DBCS
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            //Create List of employees collection object which can store list of employees
            List < Member >  members = new List < Member > ();

    //Establish the Connection to the database
    using (SqlConnection con = new SqlConnection(connectionString))
    {
    //Creating the command object by passing the stored procedure 
    SqlCommand cmd = new SqlCommand("spGetAllMembers", con);

    //Specify the command type as stored procedure
    cmd.CommandType = CommandType.StoredProcedure;

    //Open the connection
    con.Open();

    //Execute the command and stored the result in Data Reader as the method ExecuteReader
    //is going to return a Data Reader result set
    SqlDataReader rdr = cmd.ExecuteReader();

    //Read each member
    while (rdr.Read())
    {
  
    Member mem = new Member();
    mem.ID = Convert.ToInt32(rdr["Id"]);
    mem.Name = rdr["Name"].ToString();
    mem.Gender = rdr["Gender"].ToString();
    mem.City = rdr["City"].ToString();
    mem.Salary = Convert.ToDecimal(rdr["Salary"]);
    mem.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);

      members.Add(mem);
    }
    }
    //Return the list of members that is stored in the list collection
    return members;
    }
    
Stored Procedure :
    Create procedure [dbo].[spGetAllmembers]
    as
    Begin
    Select Id, Name, Gender, City, Salary, DateOfBirth
    from member
    End
    
ASP.NET MVC design pattern - EntityFramework, Business object using stored procedure with Form GET/POST for insert, Form Binding

Call Controller :
    [HttpGet]
       public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateForm(FormCollection formCollection)
        {
            Member member = new Member();
            // Retrieve form data using form collection
            member.Name = formCollection["Name"];
            member.Gender = formCollection["Gender"];
            member.City = formCollection["City"];
            member.Salary = Convert.ToDecimal(formCollection["Salary"]);
            member.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            memberBusinessLayer.AddMember(member);
            return RedirectToAction("/Index");
        }
    
Business logic :
    public void AddMember(Member member)
    {
    //Creating the connection string
    string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
    //Establishing the connection to the database
    using (SqlConnection con = new SqlConnection(connectionString))
    {
    //Creating the command object by passing the stored procedure and connection object as argument
    //This stored procedure is used to store the employee in to the database
    SqlCommand cmd = new SqlCommand("spAddMember", con)
    {
    //Specifying the command as stored procedure
    CommandType = CommandType.StoredProcedure
    };

    //Creating SQL parameters because that stored procedure accept some input values
    SqlParameter paramName = new SqlParameter
    {
    //Storing the parameter name of the stored procedure into the SQL parameter
    //By using ParameterName property
    ParameterName = "@Name",
    //storing the parameter value into sql parameter by using Value ptoperty
    Value = member.Name
    };
    //Adding that parameter into Command objects Parameter collection by using Add method
    //which will take the SQL parameter name as argument
    cmd.Parameters.Add(paramName);

    //Same for all other parameters (Gender, City, DateOfBirth )
    SqlParameter paramGender = new SqlParameter
    {
    ParameterName = "@Gender",
    Value = member.Gender
    };
    cmd.Parameters.Add(paramGender);

    SqlParameter paramCity = new SqlParameter
    {
    ParameterName = "@City",
    Value = member.City
    };
    cmd.Parameters.Add(paramCity);

    SqlParameter paramSalary = new SqlParameter
    {
    ParameterName = "@Salary",
    Value = member.Salary
    };
    cmd.Parameters.Add(paramSalary);

    SqlParameter paramDateOfBirth = new SqlParameter
    {
    ParameterName = "@DateOfBirth",
    Value = member.DateOfBirth
    };
    cmd.Parameters.Add(paramDateOfBirth);

    //Open the connection and execute the command on ExecuteNonQuery method
    con.Open();
    cmd.ExecuteNonQuery();
    }
    }
    
Stored Procedure :
  Create procedure [dbo].[spAddMember]  
  @Name nvarchar(50),
  @Gender nvarchar (10),
  @City nvarchar (50),
  @Salary decimal(18,2),
  @DateOfBirth DateTime
As
Begin
  Insert into member (Name, Gender, City, Salary, DateOfBirth)
  Values (@Name, @Gender, @City,@Salary, @DateOfBirth)
End
ASP.NET MVC design pattern - EntityFramework, Business object using stored procedure with GET/POST for insert, UpdateModel/TryUpdateModel Databinding and No Parameter

Call Controller :
      [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                Member member = new Member();
                //Data binding with UpdateModel or TryUpdateModel
                TryUpdateModel <Member> (member);
                memberBusinessLayer.AddMember(member);
                return RedirectToAction("/Index");
            }
            return View();
        }
   
Business logic :
    public void AddMember(Member member)
    .... as the same
    
Stored procedure :
    Create procedure [dbo].[spAddMember] 
    .... as the same
    
ASP.NET MVC design pattern - EntityFramework, Business object using stored procedure with GET/POST for insert, Parameter

      //Data binding
        [HttpGet]
        [ActionName("CreateParam")]
        public ActionResult CreateParam_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CreateParam")]
        public ActionResult CreateParam_Post(Member member)
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                memberBusinessLayer.AddMember(member);
                return RedirectToAction("/Index");
            }
            else
            {
                return View();
            }
        }
   
Business logic :
    public void AddMember(Member member)
    .... as the same
    
Stored procedure :
    Create procedure [dbo].[spAddMember] 
    .... as the same
    
ASP.NET MVC design pattern - EntityFramework, Business object using stored procedure with GET/POST for Update, Parameter

      [HttpGet]
        public ActionResult Edit(int id)
        {
            MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
            Member member = memberBusinessLayer.GetAllMembers().FirstOrDefault(emp => emp.ID == id);
            return View(member);
        }
        [HttpPost]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                MemberBusinessLayer memberBusinessLayer = new MemberBusinessLayer();
                memberBusinessLayer.UpdateMember(member);
                return RedirectToAction("/Index");
            }
            return View(member);
        }
    
Business logic :
      public void UpdateMember(Member member)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdatemember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = member.ID;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = member.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@Gender";
                paramGender.Value = member.Gender;
                cmd.Parameters.Add(paramGender);

                SqlParameter paramCity = new SqlParameter();
                paramCity.ParameterName = "@City";
                paramCity.Value = member.City;
                cmd.Parameters.Add(paramCity);

                SqlParameter paramSalary = new SqlParameter();
                paramSalary.ParameterName = "@Salary";
                paramSalary.Value = member.Salary;
                cmd.Parameters.Add(paramSalary);

                SqlParameter paramDateOfBirth = new SqlParameter();
                paramDateOfBirth.ParameterName = "@DateOfBirth";
                paramDateOfBirth.Value = member.DateOfBirth;
                cmd.Parameters.Add(paramDateOfBirth);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
Stored procedure :
Create procedure [dbo].[spUpdatemember]
  @Id int,
  @Name nvarchar(50),      
  @Gender nvarchar (10),      
  @City nvarchar (50), 
  @Salary decimal(18,2),     
  @DateOfBirth DateTime 
as      
Begin      
  Update member Set
    Name = @Name,
    Gender = @Gender,
    City = @City,
    Salary = @Salary,
    DateOfBirth = @DateOfBirth
  Where   Id = @Id
End