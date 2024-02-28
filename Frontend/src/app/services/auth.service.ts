import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserFromLogin, UserFromRegister } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http:HttpClient) { }

authUser(user: UserFromLogin) {
  const baseurl=environment.baseUrl;
  return this.http.post(baseurl+"/account/login",user);
  // let UserArray = [];
  // if (localStorage.getItem('Users')) {
  //   UserArray = JSON.parse(localStorage.getItem('Users'));
  // }
  // return UserArray.find(p => p.userName === user.userName && p.password === user.password);
}
RegisterUser(user:UserFromRegister){
  const baseUrl=environment.baseUrl;
  return this.http.post(baseUrl+"/account/register",user);

}
isLoggedIn(): boolean {
  
  return !!localStorage.getItem('token'); // Assuming 'token' is set upon successful login
}

}
