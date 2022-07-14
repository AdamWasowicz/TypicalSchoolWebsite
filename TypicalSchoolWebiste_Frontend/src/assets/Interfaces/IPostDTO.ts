import IUserDTO from "./IUserDTO"

export default interface IPostDTO {
    Id : number
    Title: string
    TextContent: string
    CreationDate: Date
    LastEditDate: Date
    ImageStorageName: string
    User: IUserDTO
    IsActive: boolean
}