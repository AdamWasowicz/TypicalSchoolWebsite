import IUserDTO from "./IUserDTO"


export default interface IPostDTO {

    id : number
    title: string
    textContent: string
    accessName: string
    creationDate: Date
    lastEditDate: Date
    imageStorageName: string
    user: IUserDTO
    isActive: boolean
}