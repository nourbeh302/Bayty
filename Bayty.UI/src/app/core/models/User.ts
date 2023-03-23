import { Gender } from "../enums/Gender";
import { Role } from "../enums/Role";
import { Account } from "./Account";

export class User extends Account {
  constructor(
    public userId: string = "",
    public firstName: string = "",
    public lastName: string = "",
    public gender: Gender | null = null
  ) { 
    super()
    this.role = Role.User
  }
}