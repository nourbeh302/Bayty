import { Role } from "../enums/Role";

export class Account {
  constructor(
    public email: string = "",
    public password: string = "",
    public profileImage: string = "",
    public role: Role | null = null,
    public address: string = "",
    public phoneNumber: string = ""
  ) { }
}