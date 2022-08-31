<template>
	<div ref="ButtonWrapperElement" :class="Bem" tabindex="-1" @focusout="focusout">
		<div class="ssp-button__cw">
			<ButtonBody :active="active" :borderless="borderless" :plain="plain" :type="type" :icon="icon"
				:disabled="disabled" :family="family" :menu="menu" :tooltip="tooltip" :size="size" @click="click"
				@contextmenu="contextMenu">
				<slot></slot>
			</ButtonBody>
			<ButtonIcon v-if="menu && menu.Split" :class="SplitMenuBem" :family="family" :icon="SplitMenuIcon"
				@click="splitButtonClick" />

		</div>
		<ButtonContextMenu v-if="contextMenuCanShown && IsMenuActive" :items="menu?.Items" :size="size" :type="type"
			@item-click="contextMenuItemClick" />
	</div>

</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { SspContextMenuItem } from '@/store/pinia/types/ContextMenu';
export default defineComponent({
	name: "SspButton"
})

</script>

<script lang="ts" setup>
import { computed, toRefs, ref } from '@vue/reactivity';
import propsObj from "./BaseButton/js/props"

import ButtonBody from "./BodyButton/index.vue"
import ButtonContextMenu from "./ContextMenuButton/index.vue"
import ButtonIcon from "./IconButton/index.vue"

const emits = defineEmits(["context-menu-item-click", "contextmenu", "click"])

const props = defineProps(propsObj)
const { active, borderless, type, tooltip, disabled, family, menu, size } = toRefs(props)

const ButtonWrapperElement = ref<HTMLElement | null>(null)

let IsMenuActive = ref(false)

const Bem = computed<Array<string>>(() => {
	const base = "ssp-button"
	const result = [base]

	if (props.active)
		result.push(`${base}_active`)
	if (props.disabled)
		result.push(`${base}_disabled`)
	if (props.menu && props.menu.Split)
		result.push(`${base}_split-menu`)
	result.push(`${base}_size_${props.size}`)
	return result;
})

const SplitMenuBem = computed<Array<string>>(() => {
	const base = "ssp-button-split-menu-icon"

	let result = [base]
	result.push(`${base}__type_${props.type}`)
	result.push(`${base}__family_${props.family}`)
	if (props.plain)
		result.push(`${base}__plain`)
	return result
})

const contextMenuCanShown = computed<boolean>(() => {
	let itemsLength = menu?.value?.Items?.length ?? 0
	return !props.disabled && itemsLength > 0
})

const SplitMenuIcon = computed<string>(() => {
	if (props.family == "awesome") {
		return "angle-down"
	}
	return "more_vert"
})

function click(e: Event) {
	if (!props.disabled) {
		if (contextMenuCanShown.value && !props.menu?.Split) {
			IsMenuActive.value = true
			emits("contextmenu", e)
		}
		else {
			emits("click", e)
		}
	}
}

function splitButtonClick() {
	if (contextMenuCanShown.value) {
		IsMenuActive.value = true
	}
}

function focusout(e: FocusEvent) {
	if (e.relatedTarget == null || !ButtonWrapperElement?.value?.contains(e.relatedTarget as Node)) {
		IsMenuActive.value = false
	}
}

function contextMenu(e: Event) {
	if (contextMenuCanShown.value) {
		e.preventDefault()
		IsMenuActive.value = true
	}

	emits("contextmenu", e)
}

function contextMenuItemClick(item: SspContextMenuItem) {
	emits("context-menu-item-click", item)
	IsMenuActive.value = false
}
</script>

<style lang="scss">
@import "./BaseButton/style"
</style>