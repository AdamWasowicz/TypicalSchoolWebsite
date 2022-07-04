import { IconDefinition } from "@fortawesome/fontawesome-svg-core"

export default interface INavBarElement {
    textContent: string
    onClickHandler: () => void
    iconName: IconDefinition
}
