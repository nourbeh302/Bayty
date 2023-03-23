import { Role } from "../enums/Role";
import { Account } from "./Account";

export class Corporation extends Account {
  constructor(
    public corporationId: string = "",
    public corporationName: string = "",
    public taxpayerIdentificationNumber: string = ""
  ) { 
    super()
    this.role = Role.Corporation
  }
}