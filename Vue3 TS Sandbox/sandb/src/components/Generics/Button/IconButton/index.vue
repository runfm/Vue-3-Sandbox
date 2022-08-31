<template>
	<div class="ssp-icon-wrapper">
		<span v-if="family == 'material'" class="ssp-icon-mi material-icons-round">
			{{ icon }}
		</span>

		<i v-else :class="`fa fa-${icon}`" />

	</div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({
	name:"SspButtonIcon"
})
</script>

<script lang="ts" setup>
import { computed } from '@vue/reactivity';
import { toRefs } from 'vue';

const props = defineProps({
	icon: String,
	family: {
		type: String,
		default: "material" /* element, material, awesome */
	},
})

const { icon, family } = toRefs(props)



</script>

<style lang="scss">
.ssp-icon-wrapper {
	display: flex;
	align-items: center;

	.ssp-icon-mi{
		font-size: 16px;
	}
}

</style>