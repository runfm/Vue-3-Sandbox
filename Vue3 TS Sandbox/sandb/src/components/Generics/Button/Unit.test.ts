import { mount } from '@vue/test-utils'
import ElementPlus from 'element-plus'

import ButtonComponent from "@/components/Generics/Button/index.vue"
import { reactive } from 'vue';


test("render", async () => {

  const menuSettings = {
    Split: true,
    Items: [
      {
        ID: "111",
        Name: "CM 1"
      },
      {
        ID: "112",
        Name: "CM 2"
      }
    ]
  };

  const slotText = "Main Content DG"

  let options = reactive({
    global: {
      plugins: [ElementPlus],
    },
    props: {
      icon: "home",
      menu: menuSettings
    },
    slots: {
      default: slotText
    },
    data() {
      return { /* count: 0 */ }
    }
  })

  const wrapper = mount(ButtonComponent, options)
  expect(wrapper.text()).toContain(slotText)

  const buttonBody = wrapper.find(".ssp-button-body")

  let menu = wrapper.find(".ssp-button-context-menu")
  expect(menu.exists()).toBeFalsy()

  await buttonBody.trigger('contextmenu')
  menu = wrapper.find(".ssp-button-context-menu")
  expect(menu.exists()).toBeTruthy()

  let menuItem = wrapper.find(".ssp-button-context-menu-item")
  expect(menuItem.exists()).toBeTruthy()

  await menuItem.trigger("click", {
    ID: "111",
    Name: "CM 1"
  })

  menu = wrapper.find(".ssp-button-context-menu")
  expect(menu.exists()).toBeFalsy()

  //console.log(wrapper.emitted("context-menu-item-click"))

  expect(wrapper.emitted()).toHaveProperty("context-menu-item-click")

  let splitMenuButton = wrapper.find(".ssp-button-split-menu-icon")

  expect(splitMenuButton.exists()).toBeTruthy()


})



