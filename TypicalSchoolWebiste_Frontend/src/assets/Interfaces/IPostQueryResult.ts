import IPostDTO from "./IPostDTO"

export default interface IPostQueryResult {
    desiredPage : number
    desiredNumberOfItems : number
    maxPages : number
    posts : Array<IPostDTO>
}