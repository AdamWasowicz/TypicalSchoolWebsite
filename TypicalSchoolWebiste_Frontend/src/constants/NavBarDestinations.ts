//Interfaces
import { faBookOpen, faGraduationCap, faImage, faInfo, faPerson, faQuestion, faSchool, faSquare } from "@fortawesome/free-solid-svg-icons";
import INavBarElement from "../assets/Interfaces/INavBarElement";

const navBarDestinations: Array<INavBarElement> = [
    {
        textContent: "O nas",
        onClickHandler: () => {return alert("test")},
        iconName: faSquare
    },

    {
        textContent: "Galeria",
        onClickHandler: () => {return alert("test")},
        iconName: faImage
    },

    {
        textContent: "Kontakt",
        onClickHandler: () => {return alert("test")},
        iconName: faInfo
    },

    {
        textContent: "Szkoła",
        onClickHandler: () => {return alert("test")},
        iconName: faSchool
    },

    {
        textContent: "Kandydaci",
        onClickHandler: () => {return alert("test")},
        iconName: faPerson
    },

    {
        textContent: "Uczniowie",
        onClickHandler: () => {return alert("test")},
        iconName: faGraduationCap
    },

    {
        textContent: "Rodzice",
        onClickHandler: () => {return alert("test")},
        iconName: faQuestion
    },

    {
        textContent: "E-SZKOŁA",
        onClickHandler: () => {return alert("test")},
        iconName: faBookOpen
    },
]

export default navBarDestinations;