# ConferenceRoomRent
<h2>How to run?</h2>
<p>To run this project, you need to:</p>
<ol>
  <li>
    Install project on your machine
  </li>
  <li>
    Change server name in application.json to that one your machine has
  </li>
  <li>
    Run the app, all migrations will be applied automatically :)
  </li>
</ol>
<h2>Endpoints</h2>
<p>API runs on https://localhost:7226 (changable) and has 2 controllers: UserController and TaskController. Lets walk over their endpoints:</p>
<h3>AuthController</h3>
<ul>
  <li>
    /api/auth/login - POST, allows users to log in using LoginRequestDto, which consists of Username and Password fields. If login was successful, user gets back responce with token inside, which they can use to authorize and access TaskController endpoints.
  </li>
  <li>
    /api/auth/register - POST, allows user to register using RegistrationRequestDto, which consists of Email, Password, Name, PhoneNumber and Role fields. If success, user is created and saved in db.
  </li>
  <li>
    /api/auth/AssignRole - POST, allows to assign role to user using same RegistrationRequestDto, where Email and Role fields will be used to add user to role.
  </li>
</ul>
<b>Note: Every controller except AuthController requers authorization</b>
<h3>UtilityController</h3>
<ul>
  <li>
    /api/utilities - GET, returns list of all utilities.
  </li>
  <li>
    /api/utilities - POST, creates new utility, only users with <b>admin</b> role can access it.
  </li>
  <li>
    /api/utilities - PUT, updates utility based on Id, only users with <b>admin</b> role can access it.
  </li>
  <li>
    /api/utilities/{id:int} - GET, returns utility that has same Id as in request. 
  </li>
  <li>
    /api/utilities/{id:int} - DELETE, removes utility that has same Id as in request from db. 
  </li>
</ul>
<h3>ConferenceRoomController</h3>
<ul>
  <li>
    /api/conferenceroom - GET, returns list of all conference rooms.
  </li>
  <li>
    /api/conferenceroom - POST, creates new conference room, only users with <b>admin</b> role can access it.
  </li>
  <li>
    /api/conferenceroom - PUT, updates conference room based on Id, only users with <b>admin</b> role can access it.
  </li>
  <li>
    /api/conferenceroom/{id:int} - GET, returns conference room that has same Id as in request. 
  </li>
  <li>
    /api/conferenceroom/{id:int} - DELETE, removes conference room that has same Id as in request from db. 
  </li>
</ul>
<h3>RentController</h3>
<ul>
  <li>
    /api/conferenceroom - GET, returns list of all rent entities.
  </li>
  <li>
    /api/conferenceroom - POST, creates new rent entity based on list of utilities, DateTime of start and end of rent and Id of conference room.
  </li>
  <li>
    /api/conferenceroom - PUT, updates rent entity based on Id, list of utilities, DateTime of start and end of rent and Id of conference room, only users with <b>admin</b> role and user that has same Id as UserId field of given entity can access it.
  </li>
  <li>
    /api/conferenceroom/{id:int} - GET, returns conference room that has same Id as in request. 
  </li>
  <li>
    /api/conferenceroom/{id:int} - DELETE, removes rent entity that has same Id as in request from db, only users with <b>admin</b> role and user that has same Id as UserId field of given entity can access it. 
  </li>
</ul>
