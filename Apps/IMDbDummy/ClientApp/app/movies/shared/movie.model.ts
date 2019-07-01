import { IProducer } from "./producer.model";
import { IActor } from "./actor.model";

export interface IMovie{
    id: number
    name: string
    yearOfRelease: number
    plot: string
    posterUrl: string
    producer : IProducer
    movieActors: IActor[]
}