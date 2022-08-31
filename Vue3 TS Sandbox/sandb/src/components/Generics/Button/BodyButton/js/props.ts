import { SspContextMenu } from "@/store/pinia/types/ContextMenu"
import { PropType } from "vue"




export default {
  active: Boolean,
  borderless: Boolean,
  blank:Boolean,
  disabled:Boolean,
  plain: Boolean,
  tooltip:String,
  icon:String,
  href:String,
  type:{
    type:String,
    default:"default"
  }, /* default, primary, success, info, warning, danger */
  menu:Object as PropType <SspContextMenu>,
  family:{
    type:String,
    default:"material" /* element, material, awesome */
  },
  size: {
    type: String,
    default: "medium" /* large, medium, small, mini */
  }
}