import IPostDTO from "./IPostDTO"

export default interface IPostQueryResult {
    DesiredPage : number
    DesiredNumberOfItems : number
    MaxPages : number
    Posts : Array<IPostDTO>
}